using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    // Esta es la referencia al prefab de la tubería que vamos a instanciar
    public GameObject pipePrefab;

    // El rango de alturas en el que las tuberías pueden aparecer
    public float heightRange = 0.5f;

    // El tiempo máximo que debe pasar antes de generar una nueva tubería
    public float maxTime = 1.75f;

    // Un temporizador para controlar el tiempo entre generación de tuberías
    private float timer;

    // Este es el "pool" que va a manejar las tuberías para que no las tengamos que destruir
    private ObjectPool pipePool;

    void Start()
    {
        // Aquí inicializamos el pool de objetos (con 10 objetos por defecto)
        pipePool = new ObjectPool(pipePrefab, 10);
        SpawnPipe(); // Llamamos a la función para generar una tubería al inicio
    }

    void Update()
    {
        // Vamos sumando el tiempo que pasa para ver cuándo generamos una nueva tubería
        timer += Time.deltaTime;

        // Si ya pasó el tiempo máximo, generamos una nueva tubería
        if (timer > maxTime)
        {
            SpawnPipe();  // Generamos la tubería
            timer = 0;    // Reiniciamos el temporizador
        }
    }

    // Esta función genera una nueva tubería
    public void SpawnPipe()
    {
        // Sacamos una tubería del pool de objetos (si hay alguna disponible)
        GameObject newPipe = pipePool.GetObject();

        if (newPipe != null)
        {
            // Elegimos una posición aleatoria para la tubería dentro del rango especificado
            Vector3 spawnPosition = transform.position + new Vector3(0, UnityEngine.Random.Range(-heightRange, heightRange));
            newPipe.transform.position = spawnPosition;  // Colocamos la tubería en esa posición
            newPipe.SetActive(true); // Activamos la tubería para que aparezca en la escena

            // Después de un tiempo, desactivamos la tubería (es como si desapareciera)
            StartCoroutine(DeactivatePipeAfterTime(newPipe, 15f));
        }
    }

    // Esta función desactiva la tubería después de un tiempo específico (15 segundos)
    private IEnumerator DeactivatePipeAfterTime(GameObject pipe, float time)
    {
        yield return new WaitForSeconds(time);  // Esperamos el tiempo que le pasemos
        pipe.SetActive(false);  // Desactivamos la tubería (la "desaparecemos")
    }
}

// Esta es la clase que maneja el pool de objetos (es decir, el grupo de tuberías reutilizables)
public class ObjectPool
{
    private GameObject prefab;  // El objeto que vamos a reutilizar (el prefab de la tubería)
    private Queue<GameObject> pool;  // Una cola donde vamos a almacenar las tuberías

    // El constructor inicializa el pool con un número de objetos que se pueden reutilizar
    public ObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;  // Guardamos el prefab que se va a usar
        pool = new Queue<GameObject>();  // Creamos la cola que va a manejar los objetos

        // Llenamos el pool con una cantidad inicial de objetos (desactivados al principio)
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);  // Creamos una nueva tubería
            obj.SetActive(false);  // La dejamos desactivada para que no aparezca aún
            pool.Enqueue(obj);  // La agregamos al pool
        }
    }

    // Esta función obtiene un objeto del pool para usarlo
    public GameObject GetObject()
    {
        // Si hay objetos disponibles en el pool, los sacamos y los devolvemos
        if (pool.Count > 0)
        {
            return pool.Dequeue();  // Devolvemos el primer objeto del pool
        }
        else
        {
            // Si el pool está vacío, creamos uno nuevo (si es necesario)
            GameObject obj = Object.Instantiate(prefab);  // Creamos uno nuevo
            obj.SetActive(false);  // Lo desactivamos para no aparecer inmediatamente
            return obj;
        }
    }

    // Esta función devuelve un objeto al pool para que lo podamos reutilizar más tarde
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // Lo desactivamos para que no aparezca en la escena
        pool.Enqueue(obj);  // Lo metemos de nuevo en el pool
    }
}
