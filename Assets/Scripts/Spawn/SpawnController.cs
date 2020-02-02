using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Entities amount")]
    [SerializeField] private List<EntitySpawnAmount> entityToSpawn = new List<EntitySpawnAmount>();

    [Header("Spawn points")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private List<GameObject> enemies = new List<GameObject>();

    [Header("Spawn delays")]
    [SerializeField] private float delayMin = 0.25f;
    [SerializeField] private float delayMax = 1f;

    private bool currentlySpawning = false;

    public void Awake()
    {
        TurnOffSpawnSprites();
        CreateEntityToSpawnList();

        Spawn();
    }

    private void TurnOffSpawnSprites()
    {
        if (spawnPoints.Count > 0)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                SpriteRenderer spriteRenderer = spawnPoints[i].gameObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }
    }



    private void CreateEntityToSpawnList()
    {
        // Get all enemies from all lists
        for (int i = 0; i < entityToSpawn.Count; i++)
        {
            for (int j = 0; j < entityToSpawn[i].quantityToSpawn; j++)
            {
                if (entityToSpawn[i].entityType != null)
                {
                    enemies.Add(entityToSpawn[i].entityType);
                }

                else Debug.LogWarning(this + ": you must add prefabs to spawn.");
            }
        }

        Shuffle(enemies);
    }


    public void Shuffle(List<GameObject> _list)
    {
        var count = _list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = _list[i];
            _list[i] = _list[r];
            _list[r] = tmp;
        }
    }


    public void Spawn()
    {
       StartCoroutine(StartSpawnCycle());
    }


    private IEnumerator StartSpawnCycle()
    {
        int spawnIndex = 0;
        int enemyQuantity = CalculateHowManyToSpawn();

        if (enemyQuantity == 0)
        {
            Debug.LogError(this + ": enemy quantity is 0. Can't spawn anything.");
        }

        else
        {
            for (int i = 0; i < enemyQuantity; i++)
            {
                if (spawnIndex < enemyQuantity)
                {
                    yield return new WaitForSeconds(CalculateSpawnDelay());

                    SpawnEnemy(enemies[spawnIndex], ChooseSpawnPosition(), this.transform.rotation);

                    spawnIndex += 1;
                }
            }
        }

    }


    private float CalculateSpawnDelay()
    {
        float randomDelay = Random.Range(delayMin, delayMax);
        return randomDelay;
    }


    private int CalculateHowManyToSpawn()
    {
        int totalSpawnAmount = 0;

        for (int i = 0; i < entityToSpawn.Count; i++)
        {
            totalSpawnAmount += entityToSpawn[i].quantityToSpawn;
        }

        return totalSpawnAmount;
    }


    private Vector3 ChooseSpawnPosition()
    {
        if (spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count - 1);
            return spawnPoints[randomIndex].position;
        }

        else
        {
            Debug.Log(this + ": can't choose spawn point. List empty. Returned null.");
            return this.transform.position;
        }
    }

    private void SpawnEnemy(GameObject _gameObject, Vector3 _position, Quaternion _rotation)
    {
        Instantiate(_gameObject, _position, _rotation);
    }

}
