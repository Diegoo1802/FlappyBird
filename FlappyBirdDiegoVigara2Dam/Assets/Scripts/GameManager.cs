using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements; // Importa la librer�a para manejar anuncios en Unity Ads

public class GameManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public GameObject gameoverCanvas; // Referencia al canvas de game over
    private int deathCount; // Variable para contar el n�mero de muertes
    private int adThresholdMin = 3; // M�nimo de muertes para mostrar un anuncio
    private int adThresholdMax = 5; // M�ximo de muertes para mostrar un anuncio

    private string adUnitId = "Interstitial_Android"; // ID del anuncio que vamos a mostrar (en este caso, intersticial)

    // Se ejecuta cuando inicia el juego
    void Start()
    {
        // Inicializa Unity Ads. El primer par�metro es el ID de tu proyecto Unity Ads.
        Advertisement.Initialize("5780387", true, this);

        // Cargar el contador de muertes desde PlayerPrefs. Si no existe, devuelve 0.
        deathCount = PlayerPrefs.GetInt("deathCount", 0);
        Debug.Log($"Contador de muertes cargado: {deathCount}");

        Time.timeScale = 1; // Asegura que el tiempo no est� pausado al inicio
    }

    // M�todo que se llama cuando el jugador muere
    public void RegisterDeath()
    {
        deathCount++; // Incrementa el contador de muertes
        PlayerPrefs.SetInt("deathCount", deathCount); // Guarda el contador de muertes en PlayerPrefs

        Debug.Log($"N�mero de muertes: {deathCount}");

        // Si el n�mero de muertes est� en el rango de 3 a 5, muestra un anuncio
        if (deathCount >= Random.Range(adThresholdMin, adThresholdMax))
        {
            deathCount = 0; // Reinicia el contador de muertes despu�s de mostrar un anuncio
            PlayerPrefs.SetInt("deathCount", deathCount); // Actualiza el contador en PlayerPrefs
            ShowAd(); // Muestra el anuncio
        }
    }

    // M�todo que se llama cuando el juego termina
    public void GameOver()
    {
        gameoverCanvas.SetActive(true); // Activa el canvas de Game Over
        Time.timeScale = 0; // Pausa el juego (tiempo en 0)

        RegisterDeath(); // Llama a RegisterDeath para gestionar las muertes y mostrar anuncios
    }

    // M�todo para reiniciar el juego
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Vuelve a cargar la escena actual
    }

    // M�todo para mostrar un anuncio
    private void ShowAd()
    {
        // Si el anuncio est� listo, lo muestra
        
            Advertisement.Show(adUnitId, this); // Muestra el anuncio intersticial
        
    }

    // M�todos de IUnityAdsShowListener (son necesarios para escuchar eventos de los anuncios)
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

    // M�todos de IUnityAdsInitializationListener (se ejecutan cuando Unity Ads se inicializa)
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads se inicializ� correctamente.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Error al inicializar Unity Ads: {error} - {message}");
    }
}
