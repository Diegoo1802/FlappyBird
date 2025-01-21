using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    // Esta es la referencia al prefab de la tuber�a que vamos a instanciar
    public GameObject pipePrefab;

    // El rango de alturas en el que las tuber�as pueden aparecer
    public float heightRange = 0.5f;

    // El tiempo m�ximo que debe pasar antes de generar una nueva tuber�a
    public float maxTime = 1.75f;

    // Un temporizador para controlar el tiempo entre generaci�n de tuber�as
    private float timer;

    // Este es el "pool" que va a manejar las tuber�as para que no las tengamos que destruir
    private ObjectPool pipePool;

    void Start()
    {
        // Aqu� inicializamos el pool de objetos (con 10 objetos por defecto)
        pipePool = new ObjectPool(pipePrefab, 10);
        SpawnPipe(); // Llamamos a la funci�n para generar una tuber�a al inicio
    }

    void Update()
    {
        // Vamos sumando el tiempo que pasa para ver cu�ndo generamos una nueva tuber�a
        timer += Time.deltaTime;

        // Si ya pas� el tiempo m�ximo, generamos una nueva tuber�a
        if (timer > maxTime)
        {
            SpawnPipe();  // Generamos la tuber�a
            timer = 0;    // Reiniciamos el temporizador
        }
    }

    // Esta funci�n genera una nueva tuber�a
    public void SpawnPipe()
    {
        // Sacamos una tuber�a del pool de objetos (si hay alguna disponible)
        GameObject newPipe = pipePool.GetObject();

        if (newPipe != null)
        {
            // Elegimos una posici�n aleatoria para la tuber�a dentro del rango especificado
            Vector3 spawnPosition = transform.position + new Vector3(0, UnityEngine.Random.Range(-heightRange, heightRange));
            newPipe.transform.position = spawnPosition;  // Colocamos la tuber�a en esa posici�n
            newPipe.SetActive(true); // Activamos la tuber�a para que aparezca en la escena

            // Despu�s de un tiempo, desactivamos la tuber�a (es como si desapareciera)
            StartCoroutine(DeactivatePipeAfterTime(newPipe, 15f));
        }
    }

    // Esta funci�n desactiva la tuber�a despu�s de un tiempo espec�fico (15 segundos)
    private IEnumerator DeactivatePipeAfterTime(GameObject pipe, float time)
    {
        yield return new WaitForSeconds(time);  // Esperamos el tiempo que le pasemos
        pipe.SetActive(false);  // Desactivamos la tuber�a (la "desaparecemos")
    }
}

// Esta es la clase que maneja el pool de objetos (es decir, el grupo de tuber�as reutilizables)
public class ObjectPool
{
    private GameObject prefab;  // El objeto que vamos a reutilizar (el prefab de la tuber�a)
    private Queue<GameObject> pool;  // Una cola donde vamos a almacenar las tuber�as

    // El constructor inicializa el pool con un n�mero de objetos que se pueden reutilizar
    public ObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;  // Guardamos el prefab que se va a usar
        pool = new Queue<GameObject>();  // Creamos la cola que va a manejar los objetos

        // Llenamos el pool con una cantidad inicial de objetos (desactivados al principio)
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);  // Creamos una nueva tuber�a
            obj.SetActive(false);  // La dejamos desactivada para que no aparezca a�n
            pool.Enqueue(obj);  // La agregamos al pool
        }
    }

    // Esta funci�n obtiene un objeto del pool para usarlo
    public GameObject GetObject()
    {
        // Si hay objetos disponibles en el pool, los sacamos y los devolvemos
        if (pool.Count > 0)
        {
            return pool.Dequeue();  // Devolvemos el primer objeto del pool
        }
        else
        {
            // Si el pool est� vac�o, creamos uno nuevo (si es necesario)
            GameObject obj = Object.Instantiate(prefab);  // Creamos uno nuevo
            obj.SetActive(false);  // Lo desactivamos para no aparecer inmediatamente
            return obj;
        }
    }

    // Esta funci�n devuelve un objeto al pool para que lo podamos reutilizar m�s tarde
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // Lo desactivamos para que no aparezca en la escena
        pool.Enqueue(obj);  // Lo metemos de nuevo en el pool
    }
}
