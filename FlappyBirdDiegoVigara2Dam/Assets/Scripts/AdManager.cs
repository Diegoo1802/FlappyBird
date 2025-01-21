using System.Diagnostics;
using UnityEngine;
using UnityEngine.Advertisements;  // Aseg�rate de tener esto

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "yourGameID";  // Reempl�zalo con tu Game ID de Unity Ads
    private string interstitialPlacementId = "interstitial";  // ID del anuncio intersticial
    private string rewardPlacementId = "rewardedVideo";  // ID del anuncio premiado
    private bool testMode = true;  // Establece en 'false' en la versi�n de producci�n

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
            Debug.Log("El anuncio intersticial no est� listo.");
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
            Debug.Log("El anuncio premiado no est� listo.");
        }
    }

    // M�todos del IUnityAdsListener
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
