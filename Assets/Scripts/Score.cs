using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int score; // Declarar la variable score para almacenar la puntuación actual.

    // Start is called before the first frame update
    void Start()
    {
        score = 0; // Inicializar el score.
        scoreText.text = score.ToString(); // Mostrar la puntuación inicial.
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString(); // Mostrar la mejor puntuación almacenada.
    }

    // Update is called once per frame
    void Update()
    {
        // Aquí puedes agregar lógica para actualizar el puntaje si es necesario.
    }

    public void UpdateScore(int points)
    {
        score += points; // Incrementar el score.
        scoreText.text = score.ToString(); // Actualizar el texto de la puntuación.
        UpdateBestScore(); // Verificar y actualizar la mejor puntuación si es necesario.
    }

    public void UpdateBestScore()
    {
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score); // Guardar el nuevo puntaje más alto.
            bestScoreText.text = score.ToString(); // Actualizar el texto de la mejor puntuación.
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        UpdateBestScore();
    }
}
