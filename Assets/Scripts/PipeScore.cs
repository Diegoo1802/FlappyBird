using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScore : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Buscar el componente Score y actualizar el puntaje
            var scoreManager = FindObjectOfType<Score>();
            if (scoreManager != null)
            {
                scoreManager.UpdateScore(1); // Aumenta el puntaje en 1
            }

            // Reproducir el sonido
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
