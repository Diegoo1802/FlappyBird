using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverCanvas;
    private int deathCount = 0;
    private int adThreshold = 3; // Mínimo de muertes para mostrar un anuncio
    private AdManager adManager;

    void Start()
    {
        // Obtener el componente AdManager en la escena
        adManager = FindObjectOfType<AdManager>();
        Time.timeScale = 1;
    }

    void Update()
    {
        // Lógica adicional si es necesario
    }

    public void GameOver()
    {
        gameoverCanvas.SetActive(true);
        Time.timeScale = 0;

        deathCount++;
        if (deathCount >= adThreshold) // Mostrar anuncio cuando mueren 3 veces
        {
            adManager.ShowAd(); // Llamada a ShowAd en el AdManager
            deathCount = 0; // Reinicia el contador
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
