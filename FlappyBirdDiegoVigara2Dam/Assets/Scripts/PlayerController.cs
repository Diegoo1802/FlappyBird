using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencia al GameManager que controla el flujo del juego (por ejemplo, GameOver)
    private GameManager gameManager;

    // Este método se llama al inicio del juego
    void Start()
    {
        // Busca el objeto en la escena que tiene el componente GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    // Este método se llama cuando el jugador entra en contacto con otro objeto (trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto con el que colisiona tiene la etiqueta "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Llama al método GameOver del GameManager
            gameManager.GameOver();
        }
    }
}
