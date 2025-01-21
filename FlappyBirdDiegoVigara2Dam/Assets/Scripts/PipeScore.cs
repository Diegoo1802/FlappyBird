using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScore : MonoBehaviour
{
    // Referencia al componente AudioSource que usaremos para reproducir el sonido
    public AudioSource audioSource;

    // Este m�todo se ejecuta cuando algo entra en el �rea de colisi�n del objeto (Pipe)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el objeto que entra en la colisi�n tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Buscar el objeto Score en la escena y actualizar el puntaje
            var scoreManager = FindObjectOfType<Score>(); // Busca el script Score en la escena
            if (scoreManager != null) // Si el objeto Score existe
            {
                scoreManager.UpdateScore(1); // Aumenta el puntaje en 1
            }

            // Reproducir el sonido si la referencia al AudioSource est� configurada
            if (audioSource != null)
            {
                audioSource.Play(); // Reproduce el sonido
            }
        }
    }
}
