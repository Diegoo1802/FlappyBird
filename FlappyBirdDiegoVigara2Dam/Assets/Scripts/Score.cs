using UnityEngine;
using TMPro;  // Usamos TextMeshPro para mostrar el puntaje

public class Score : MonoBehaviour
{
    // Referencias a los objetos TextMeshPro que mostrarán el puntaje actual y el mejor puntaje
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    // Variable para almacenar el puntaje actual
    private int score;

    // Este método se ejecuta al iniciar el juego
    void Start()
    {
        // Inicializa el puntaje a 0
        score = 0;

        // Muestra el puntaje actual y el mejor puntaje en los UI
        scoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString(); // Obtiene el mejor puntaje guardado, por defecto 0
    }

    // Este método se ejecuta en cada frame (si es necesario se puede agregar lógica aquí)
    void Update()
    {
        // Aquí podrías agregar lógica adicional si es necesario.
    }

    // Este método se llama para actualizar el puntaje (por ejemplo, cuando el jugador consigue puntos)
    public void UpdateScore(int points)
    {
        // Suma los puntos al puntaje actual
        score += points;

        // Actualiza el texto del puntaje actual en el UI
        scoreText.text = score.ToString();

        // Verifica si el puntaje actual es el mejor y actualiza el mejor puntaje si es necesario
        UpdateBestScore();
    }

    // Este método se llama para verificar y actualizar el mejor puntaje
    public void UpdateBestScore()
    {
        // Si el puntaje actual es mayor que el mejor puntaje guardado
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            // Guarda el nuevo mejor puntaje
            PlayerPrefs.SetInt("BestScore", score);

            // Actualiza el texto del mejor puntaje en el UI
            bestScoreText.text = score.ToString();
        }
    }
}
