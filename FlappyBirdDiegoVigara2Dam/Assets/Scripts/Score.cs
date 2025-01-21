using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int score;

    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    void Update()
    {
        // Aquí podrías agregar lógica adicional si es necesario.
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        UpdateBestScore();
    }

    public void UpdateBestScore()
    {
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScoreText.text = score.ToString();
        }
    }
}
