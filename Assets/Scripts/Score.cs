using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int score; // Declarar la variable score para almacenar la puntuaci�n actual.

    // Start is called before the first frame update
    void Start()
    {
        score = 0; // Inicializar el score.
        scoreText.text = score.ToString(); // Mostrar la puntuaci�n inicial.
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString(); // Mostrar la mejor puntuaci�n almacenada.
    }

    // Update is called once per frame
    void Update()
    {
        // Aqu� puedes agregar l�gica para actualizar el puntaje si es necesario.
    }

    public void UpdateScore(int points)
    {
        score += points; // Incrementar el score.
        scoreText.text = score.ToString(); // Actualizar el texto de la puntuaci�n.
        UpdateBestScore(); // Verificar y actualizar la mejor puntuaci�n si es necesario.
    }

    public void UpdateBestScore()
    {
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score); // Guardar el nuevo puntaje m�s alto.
            bestScoreText.text = score.ToString(); // Actualizar el texto de la mejor puntuaci�n.
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        UpdateBestScore();
    }
}
