using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    // Velocidad con la que el suelo se moverá
    public float speed = 1.25f;

    // Ancho del suelo al que se debe ajustar el sprite
    public float width = 6;

    // Referencia al componente SpriteRenderer del objeto
    public SpriteRenderer spriteRenderer;

    // Variable para almacenar el tamaño inicial del sprite
    private Vector2 starSize;

    // Este método se llama una vez cuando se inicia el juego
    void Start()
    {
        // Guarda el tamaño inicial del sprite del suelo
        starSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    // Este método se llama una vez por cuadro (frame)
    void Update()
    {
        // Aumenta el tamaño del suelo a la derecha a lo largo del tiempo
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + speed * Time.deltaTime, spriteRenderer.size.y);

        // Cuando el tamaño del sprite supera el ancho definido, lo resetea a su tamaño inicial
        if (spriteRenderer.size.x > width)
        {
            spriteRenderer.size = starSize;
        }
    }
}
