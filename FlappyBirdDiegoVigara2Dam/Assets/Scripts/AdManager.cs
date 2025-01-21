using System.Diagnostics;
using UnityEngine;
using UnityEngine.Advertisements;  // Asegúrate de tener esto

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "yourGameID";  // Reemplázalo con tu Game ID de Unity Ads
    private string interstitialPlacementId = "interstitial";  // ID del anuncio intersticial
    private string rewardPlacementId = "rewardedVideo";  // ID del anuncio premiado
    private bool testMode = true;  // Establece en 'false' en la versión de producción

    void Start()
    {
        // Inicializa Unity Ads
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);  // Agregar el listener
    }

    // Mostrar anuncio intersticial
    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialPlacementId))
        {
            Advertisement.Show(interstitialPlacementId);
        }
        else
        {
            Debug.Log("El anuncio intersticial no está listo.");
        }
    }

    // Mostrar anuncio premiado
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewardPlacementId))
        {
            Advertisement.Show(rewardPlacementId);
        }
        else
        {
            Debug.Log("El anuncio premiado no está listo.");
        }
    }

    // Métodos del IUnityAdsListener
    public void OnUnityAdsDidError(string message) { }
    public void OnUnityAdsDidStart(string placementId) { }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Gestionar el resultado del anuncio
        if (placementId == rewardPlacementId && showResult == ShowResult.Finished)
        {
            // El jugador vio el anuncio completo, otorgar recompensa
            Debug.Log("Recompensa otorgada.");
        }
    }
}
