using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text scoreText;
    private float currentScore = 0;

    // Update is called once per frame
    void Update()
    {
        int msScore = (int)(currentScore * 1000f);
        scoreText.text = "Score: " + msScore.ToString("D3");
        currentScore += Time.deltaTime;       
    }
}
