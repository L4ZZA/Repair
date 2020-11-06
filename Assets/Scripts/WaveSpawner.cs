using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBtwSpawns;
    }
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBtwWaves;

    Wave currentWave;
    int currentWaveIndex;
    Transform playerTransform;
    bool finishedSpawning;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(startNextWave(currentWaveIndex));
    }

    private void Update()
    {
        bool noMoreEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        if(finishedSpawning && noMoreEnemies)
        {
            finishedSpawning = false;
            if(currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(startNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("GAME COMPLETED!");
            }
        }
    }

    IEnumerator startNextWave(int index)
    {
        yield return new WaitForSeconds(timeBtwWaves);
        StartCoroutine(spawnWave(index));
    }

    IEnumerator spawnWave(int index)
    {
        currentWave = waves[index];

        for(int i = 0; i < waves.Length; i++)
        {
            if(!playerTransform)
            {
                yield break;
            }
            int randomEnemyIndex = Random.Range(0, currentWave.enemies.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Enemy randomEnemy = currentWave.enemies[randomEnemyIndex];
            Transform randomSpot = spawnPoints[randomSpawnPointIndex];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            finishedSpawning = i == currentWave.count - 1;
            yield return new WaitForSeconds(currentWave.timeBtwSpawns);
        }
    }

}
