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

    [Header("Game Over")]
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text finalScoreText;

    [Header("Buttons")]
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;


    private void Awake()
    {
        ActivateObject(gameOverText.gameObject, false);
        ActivateObject(finalScoreText.gameObject, false);

        ActivateObject(buttonRestart.gameObject, false);
        ActivateObject(buttonExit.gameObject, false);

        SetUpButtons();
    }

    private void OnEnable()
    {
        ScoreController.Action_UpdatedHUDScore += ChangeScoreText;
        ScoreController.Action_ChangeTime += ChangeTimer;
        ScoreController.Action_GameOver += ChangeGameOverText;

        EntityHealth.Action_PlayerHealthChanged += ChangeHealthSlider;
    }


    private void OnDisable()
    {
        ScoreController.Action_UpdatedHUDScore -= ChangeScoreText;
        ScoreController.Action_ChangeTime -= ChangeTimer;
        ScoreController.Action_GameOver -= ChangeGameOverText;

        EntityHealth.Action_PlayerHealthChanged -= ChangeHealthSlider;
    }


    private void SetUpButtons()
    {
        if (buttonRestart != null && buttonRestart != null)
        {
            buttonRestart.onClick.AddListener(Restart);
            buttonExit.onClick.AddListener(Exit);
        }
        else
        {
            Debug.LogWarning(this + ": a button is null. Assign in inpector.");
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
        ActivateObject(gameOverText.gameObject, true);
        ActivateObject(finalScoreText.gameObject, true);
        ActivateObject(buttonRestart.gameObject, true);
        ActivateObject(buttonExit.gameObject, true);

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


    private void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }


    private void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
