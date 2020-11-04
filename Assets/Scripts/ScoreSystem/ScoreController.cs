using UnityEngine;
using System;

public class ScoreController : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int playerScore = 0;

    [Header("Timer")]
    [SerializeField] private float timer = 60;

    private bool gameFinished = false;

    private float startTimer;

    public static event Action<int>     Action_UpdatedHUDScore = delegate { };
    public static event Action          Action_EnvironmentDestroyed = delegate { };
    public static event Action<string>  Action_ChangeTime = delegate { };
    public static event Action<string>  Action_GameOver = delegate { };


    private void Awake()
    {
        startTimer = timer;
        Time.timeScale = 1;
    }


    private void OnEnable()
    {
        EntityHealth.Action_EnvironmentDied += EnvironmentDestroyed;
        EntityHealth.Action_EnemyDied += ChangePlayerScore;
        EntityHealth.Action_PlayerDied += GameOver;
    }


    private void OnDisable()
    {
        EntityHealth.Action_EnvironmentDied -= EnvironmentDestroyed;
        EntityHealth.Action_EnemyDied -= ChangePlayerScore;
        EntityHealth.Action_PlayerDied -= GameOver;
    }


    public void ChangePlayerScore(int _scoreChange)
    {
        playerScore += _scoreChange;
        UpdateHUD();
    }


    private void UpdateHUD()
    {
        Action_UpdatedHUDScore.Invoke(playerScore);
    }


    private void EnvironmentDestroyed()
    {
        playerScore = 0;
        Action_EnvironmentDestroyed.Invoke();
        GameOver();
    }


    public void Update()
    {
        timer = Mathf.Clamp(timer -= Time.deltaTime, 0, startTimer);

        var span = TimeSpan.FromSeconds(timer);
        string sTimer = span.ToString(@"mm\:ss");

        ChangeTimer(sTimer);

        if (timer == 0)
        {
            gameFinished = true;
        }
    }


    private void ChangeTimer(String _string)
    {
        if (!gameFinished)
        {
            Action_ChangeTime.Invoke(_string);
        }
        else
        {
            GameOver();
        }
    }


    private void GameOver()
    {
        Action_GameOver.Invoke("Game over!");
        Time.timeScale = 0;
    }
}
