using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    public float velocity = 2;
    public Rigidbody2D rb2D;
    public float rotationSpeed = 25;
    public AudioSource audioSource;

    void Start()
    {
    }

    void Update()
    {
        // Movimiento del p�jaro con espacio, clic o toque
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.velocity = Vector2.up * velocity;
        }

        // Rotaci�n del p�jaro seg�n la velocidad del Rigidbody
        transform.rotation = Quaternion.Euler(0, 0, rb2D.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Llamar a GameOver cuando el p�jaro colisiona
        FindObjectOfType<GameManager>().GameOver();
        audioSource.Play();
    }
}
