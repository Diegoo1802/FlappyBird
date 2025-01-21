using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public GameObject gameoverCanvas;
    private int deathCount = 0;
    private int adThresholdMin = 3; // Mínimo de muertes para mostrar un anuncio
    private int adThresholdMax = 5; // Máximo de muertes para mostrar un anuncio

    // Lista de posibles anuncios que puedes mostrar
    private string[] adUnitIds = new string[] { "Rewarded_Android" }; // Agrega más anuncios si los tienes configurados en Unity Ads

    void Start()
    {
        // Inicializa Unity Ads y pasa el IUnityAdsInitializationListener
        Advertisement.Initialize("5780387", true, this);
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
        if (deathCount >= Random.Range(adThresholdMin, adThresholdMax)) // Mostrar anuncio cuando mueren entre 3 y 5 veces
        {
            deathCount = 0; // Reinicia el contador de muertes
            ShowRandomAd(); // Muestra un anuncio aleatorio
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Función para mostrar un anuncio aleatorio
    private void ShowRandomAd()
    {
        // Si Unity Ads está inicializado, muestra un anuncio aleatorio
        if (Advertisement.isInitialized)
        {
            string randomAdUnitId = adUnitIds[Random.Range(0, adUnitIds.Length)]; // Selecciona un anuncio aleatorio de la lista
            Advertisement.Show(randomAdUnitId, this); // Muestra el anuncio aleatorio seleccionado
        }
        else
        {
            Debug.Log("Unity Ads no está inicializado.");
        }
    }

    // Implementación de la interfaz IUnityAdsShowListener
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showResult)
    {
        // Aquí puedes manejar lo que sucede después de que el anuncio se complete
        if (showResult == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("El jugador vio el anuncio completo.");
        }
        else if (showResult == UnityAdsShowCompletionState.SKIPPED)
        {
            Debug.Log("El jugador saltó el anuncio.");
        }
        else if (showResult == UnityAdsShowCompletionState.UNKNOWN)
        {
            Debug.Log("Hubo un error desconocido al mostrar el anuncio.");
        }
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("El anuncio comenzó.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("El jugador hizo clic en el anuncio.");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error al mostrar el anuncio: {error.ToString()} - {message}");
    }

    // Implementación de la interfaz IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads se inicializó correctamente.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Error al inicializar Unity Ads: {error.ToString()} - {message}");
    }
}
