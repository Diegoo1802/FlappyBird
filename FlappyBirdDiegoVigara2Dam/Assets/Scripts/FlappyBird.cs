using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    // Velocidad de impulso del pájaro cuando el jugador hace clic o presiona espacio
    public float velocity = 2;

    // Referencia al componente Rigidbody2D que maneja la física del pájaro
    public Rigidbody2D rb2D;

    // Velocidad de rotación del pájaro al volar
    public float rotationSpeed = 25;

    // Fuente de audio para reproducir sonidos al colisionar
    public AudioSource audioSource;

    void Start()
    {
        // Este método se ejecuta una vez al inicio, pero no se está utilizando aquí
    }

    void Update()
    {
        // Comprobar si se hace clic o se presiona la tecla Espacio para hacer volar al pájaro
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // Establece la velocidad del Rigidbody para hacer que el pájaro se mueva hacia arriba
            rb2D.velocity = Vector2.up * velocity;
        }

        // Rotación del pájaro según la velocidad de su Rigidbody
        // Cuanto más rápido se mueva hacia abajo, más rotará hacia abajo
        transform.rotation = Quaternion.Euler(0, 0, rb2D.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Llamar al método GameOver en el GameManager cuando el pájaro colisiona con algo
        FindObjectOfType<GameManager>().GameOver();

        // Reproducir un sonido cuando el pájaro colisiona
        audioSource.Play();
    }
}
