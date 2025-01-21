using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    // Velocidad de impulso del p�jaro cuando el jugador hace clic o presiona espacio
    public float velocity = 2;

    // Referencia al componente Rigidbody2D que maneja la f�sica del p�jaro
    public Rigidbody2D rb2D;

    // Velocidad de rotaci�n del p�jaro al volar
    public float rotationSpeed = 25;

    // Fuente de audio para reproducir sonidos al colisionar
    public AudioSource audioSource;

    void Start()
    {
        // Este m�todo se ejecuta una vez al inicio, pero no se est� utilizando aqu�
    }

    void Update()
    {
        // Comprobar si se hace clic o se presiona la tecla Espacio para hacer volar al p�jaro
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // Establece la velocidad del Rigidbody para hacer que el p�jaro se mueva hacia arriba
            rb2D.velocity = Vector2.up * velocity;
        }

        // Rotaci�n del p�jaro seg�n la velocidad de su Rigidbody
        // Cuanto m�s r�pido se mueva hacia abajo, m�s rotar� hacia abajo
        transform.rotation = Quaternion.Euler(0, 0, rb2D.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Llamar al m�todo GameOver en el GameManager cuando el p�jaro colisiona con algo
        FindObjectOfType<GameManager>().GameOver();

        // Reproducir un sonido cuando el p�jaro colisiona
        audioSource.Play();
    }
}
