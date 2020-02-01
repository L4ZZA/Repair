using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        int sceneCount = SceneManager.sceneCount;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("current scene index: " + currentScene);

        if (currentScene < sceneCount)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Debug.LogError("Did not increase scene count");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }
}
