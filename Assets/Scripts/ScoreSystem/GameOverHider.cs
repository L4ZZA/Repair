using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHider : MonoBehaviour
{
    [Header("Hide this object on GameOver?")]
    [SerializeField] private bool hideOnGameOver;

    private void OnEnable()
    {
        ScoreController.Action_GameOver += GameOver;
    }


    private void OnDisable()
    {
        ScoreController.Action_GameOver -= GameOver;
    }


    private void GameOver(string _string)
    {
        if (hideOnGameOver)
        {
            this.gameObject.SetActive(false);
        }    
    }
}
