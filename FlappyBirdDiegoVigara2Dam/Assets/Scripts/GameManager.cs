using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements; // Importa la librería para manejar anuncios en Unity Ads

public class GameManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public GameObject gameoverCanvas; // Referencia al canvas de game over
    private int deathCount; // Variable para contar el número de muertes
    private int adThresholdMin = 3; // Mínimo de muertes para mostrar un anuncio
    private int adThresholdMax = 5; // Máximo de muertes para mostrar un anuncio

    private string adUnitId = "Interstitial_Android"; // ID del anuncio que vamos a mostrar (en este caso, intersticial)

    // Se ejecuta cuando inicia el juego
    void Start()
    {
        // Inicializa Unity Ads. El primer parámetro es el ID de tu proyecto Unity Ads.
        Advertisement.Initialize("5780387", true, this);

        // Cargar el contador de muertes desde PlayerPrefs. Si no existe, devuelve 0.
        deathCount = PlayerPrefs.GetInt("deathCount", 0);
        Debug.Log($"Contador de muertes cargado: {deathCount}");

        Time.timeScale = 1; // Asegura que el tiempo no esté pausado al inicio
    }

    // Método que se llama cuando el jugador muere
    public void RegisterDeath()
    {
        deathCount++; // Incrementa el contador de muertes
        PlayerPrefs.SetInt("deathCount", deathCount); // Guarda el contador de muertes en PlayerPrefs

        Debug.Log($"Número de muertes: {deathCount}");

        // Si el número de muertes está en el rango de 3 a 5, muestra un anuncio
        if (deathCount >= Random.Range(adThresholdMin, adThresholdMax))
        {
            deathCount = 0; // Reinicia el contador de muertes después de mostrar un anuncio
            PlayerPrefs.SetInt("deathCount", deathCount); // Actualiza el contador en PlayerPrefs
            ShowAd(); // Muestra el anuncio
        }
    }

    // Método que se llama cuando el juego termina
    public void GameOver()
    {
        gameoverCanvas.SetActive(true); // Activa el canvas de Game Over
        Time.timeScale = 0; // Pausa el juego (tiempo en 0)

        RegisterDeath(); // Llama a RegisterDeath para gestionar las muertes y mostrar anuncios
    }

    // Método para reiniciar el juego
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Vuelve a cargar la escena actual
    }

    // Método para mostrar un anuncio
    private void ShowAd()
    {
        // Si el anuncio está listo, lo muestra
        
            Advertisement.Show(adUnitId, this); // Muestra el anuncio intersticial
        
    }

    // Métodos de IUnityAdsShowListener (son necesarios para escuchar eventos de los anuncios)
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showResult)
    {
        Debug.Log($"Anuncio completado: {placementId} - Resultado: {showResult}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Anuncio iniciado: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"El jugador hizo clic en el anuncio: {placementId}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error al mostrar el anuncio: {placementId} - {error.ToString()} - {message}");
    }

    // Métodos de IUnityAdsInitializationListener (se ejecutan cuando Unity Ads se inicializa)
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads se inicializó correctamente.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Error al inicializar Unity Ads: {error} - {message}");
    }
}
