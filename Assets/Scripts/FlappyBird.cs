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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb2D.velocity = Vector2.up * velocity;
        }
        transform.rotation = Quaternion.Euler(0,0,rb2D.velocity.y * rotationSpeed * Time.deltaTime * 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindAnyObjectByType<GameManager>().GameOver();
        audioSource.Play();
    }
}
