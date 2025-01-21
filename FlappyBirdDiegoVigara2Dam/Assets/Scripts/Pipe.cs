using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Velocidad a la que se mover�n los tubos
    public float speed = 0.75f;

    // Este m�todo se ejecuta una vez por cuadro (frame)
    void Update()
    {
        // Mueve el objeto (el tubo) hacia la izquierda a la velocidad especificada
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
