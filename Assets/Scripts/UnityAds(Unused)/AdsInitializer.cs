using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer :MonoBehaviour, IUnityAdsInitializationListener {
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;
    private RewardedAds ad;

    void Awake() {
        InitializeAds();
        ad = GetComponent<RewardedAds>();
    }

    public void InitializeAds() {
        _gameId = ( Application.platform == RuntimePlatform.IPhonePlayer )
            ? _iOsGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
    }

    public void OnInitializationComplete() {
        // Load a rewarded ad
        ad.LoadAd();
    }

    public void OnInitializationFailed( UnityAdsInitializationError error, string message ) {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}