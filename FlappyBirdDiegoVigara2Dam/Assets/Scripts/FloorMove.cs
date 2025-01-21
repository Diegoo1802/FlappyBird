using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    // Velocidad con la que el suelo se mover�
    public float speed = 1.25f;

    // Ancho del suelo al que se debe ajustar el sprite
    public float width = 6;

    // Referencia al componente SpriteRenderer del objeto
    public SpriteRenderer spriteRenderer;

    // Variable para almacenar el tama�o inicial del sprite
    private Vector2 starSize;

    // Este m�todo se llama una vez cuando se inicia el juego
    void Start()
    {
        // Guarda el tama�o inicial del sprite del suelo
        starSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }

    // Este m�todo se llama una vez por cuadro (frame)
    void Update()
    {
        // Aumenta el tama�o del suelo a la derecha a lo largo del tiempo
        spriteRenderer.size = new Vector2(spriteRenderer.size.x + speed * Time.deltaTime, spriteRenderer.size.y);

        // Cuando el tama�o del sprite supera el ancho definido, lo resetea a su tama�o inicial
        if (spriteRenderer.size.x > width)
        {
            spriteRenderer.size = starSize;
        }
    }
}
