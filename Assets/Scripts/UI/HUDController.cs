using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [Header("Score / Health")]
    [SerializeField] private Text scoreText;
    private int currentScore;
    [SerializeField] private Slider playerHealth;

    [Header("Timer")]
    [SerializeField] private Text timerText;

    [Header("Weapon Icon")]
    [SerializeField] private WeaponIcon weaponIcon;

    [Header("Game Over screen")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;

    [Header("Pause screen")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonQuit;

    public static string EscapeBtnName = "Cancel";

    public static bool paused = false;

    public static event Action Action_Pause;
    public static event Action Action_Resume;

    private void Awake()
    {
        ActivateObject(gameOverScreen, false);

        RegisterButtons();
    }
    void OnDestroy()
    {
        UnregisterButtons();
    }

    private void OnEnable()
    {
        ScoreController.Action_UpdatedHUDScore += ChangeScoreText;
        ScoreController.Action_ChangeTime += ChangeTimer;
        ScoreController.Action_GameOver += ChangeGameOverText;

        EntityHealth.Action_PlayerHealthChanged += ChangeHealthSlider;

        Action_Pause += ShowPauseMenu;
        Action_Resume += HidePauseMenu;
    }

    private void OnDisable()
    {
        ScoreController.Action_UpdatedHUDScore -= ChangeScoreText;
        ScoreController.Action_ChangeTime -= ChangeTimer;
        ScoreController.Action_GameOver -= ChangeGameOverText;

        EntityHealth.Action_PlayerHealthChanged -= ChangeHealthSlider;

        Action_Pause  -= ShowPauseMenu;
        Action_Resume -= HidePauseMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(EscapeBtnName))
        {
            if (paused)
            {
                Action_Resume.Invoke();
            }
            else
            {
                Action_Pause.Invoke();
            }
        }
    }


    private void RegisterButtons()
    {
        if (buttonRestart && buttonRestart && buttonQuit && buttonResume)
        {
            buttonRestart.onClick.AddListener(Restart);
            buttonExit.onClick.AddListener(MainMenu);
            buttonResume.onClick.AddListener(HidePauseMenu);
            buttonQuit.onClick.AddListener(MainMenu);
        }
        else
        {
            Debug.LogWarning(this + ": a button is null in RegisterButtons(). Assign in inpector.");
        }
    }
    private void UnregisterButtons()
    {
        if (buttonRestart && buttonRestart && buttonQuit && buttonResume)
        {
            buttonRestart.onClick.RemoveListener(Restart);
            buttonExit.onClick.RemoveListener(MainMenu);
            buttonResume.onClick.RemoveListener(HidePauseMenu);
            buttonQuit.onClick.RemoveListener(MainMenu);
        }
        else
        {
            Debug.LogWarning(this + ": a button is null in UnregisterButtons(). Assign in inpector.");
        }
    }


    private void ChangeHealthSlider(int _value)
    {
        if (playerHealth != null)
        {
            playerHealth.value = Mathf.Clamp(_value, 0, 100);
        }
        else
        {
            Debug.LogWarning(this + "slider not assigned. Can't change.");
        }
    }


    public void ChangeTimer(string _timer)
    {
        if (timerText != null)
        {
            timerText.text = _timer;
        }
        else
        {
            Debug.LogWarning(this + ": text is null. Can't change.");
        }
    }


    public void ChangeScoreText(int _score)
    {
        currentScore = _score;

        if (scoreText)
        {
            scoreText.text = _score.ToString();
        }
        else
        {
            Debug.LogWarning(this + ": text is null. Can't change.");
        }
    }

    public void ChangeFinalTextScore(Text _text, int _value, bool _toUpper)
    {
        if (_text != null)
        {
            if (_toUpper)
            {
                _text.text = "Score: ".ToUpper() + _value.ToString().ToUpper();
            }

            else
            {
                _text.text = "Score: " + _value.ToString();
            }
        }
        else
        {
            Debug.LogWarning(this + ": text is null. Can't change.");
        }
    }


    private void ChangeGameOverText(string _text)
    {
        ActivateObject(gameOverScreen, true);

        ChangeText(gameOverText, _text, true);
        ChangeFinalTextScore(finalScoreText, currentScore, true);

        ActivateObject(scoreText.gameObject, false);
        ActivateObject(playerHealth.gameObject, false);
        ActivateObject(timerText.gameObject, false);
    }


    public void ChangeHealthBar(int _health)
    {
        if (playerHealth != null)
        {
            if (_health <= 100 && _health >= 0)
            {
                playerHealth.value = _health;
            }
            else
            {
                Debug.LogWarning(this + ": _health is below 0 or above 100.");
            }
        }
        else
        {
            Debug.LogError(this + ": text is null. Can't change.");
        }
    }


    public void ChangeText(Text _text, string _value, bool _toUpper)
    {
        if (_text != null)
        {
            if (_toUpper)
            {
                _text.text = _value.ToUpper();
            }
            else
            {
                _text.text = _value;
            }
        }
        else
        {
            Debug.LogError(this + ": text is null. Can't change.");
        }
    }

    private void ActivateObject(GameObject _gameObject, bool _show)
    {
        if (_gameObject != null)
        {
            _gameObject.SetActive(_show);
        }
        else
        {
            Debug.LogError(this + ": can't show/hide. GameObject is null");
        }
    }

    /// <summary>
    /// Restart the level
    /// </summary>
    private void Restart()
    {
        HidePauseMenu();
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Go back to the main menu
    /// </summary>
    private void MainMenu()
    {
        HidePauseMenu();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Pause the gameplay.
    /// </summary>
    void ShowPauseMenu()
    {
        Pause();
        ActivateObject(pauseScreen, true);
    }

    /// <summary>
    /// Resume gameplay.
    /// </summary>
    void HidePauseMenu()
    {
        Resume();
        ActivateObject(pauseScreen, false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
    }
}
