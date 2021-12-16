using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOsAdUnitId = "Rewarded_iOS";
    string _adUnitId;

    void Awake() {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = ( Application.platform == RuntimePlatform.IPhonePlayer )
            ? _iOsAdUnitId
            : _androidAdUnitId;

    }

    // Load content to the Ad Unit:
    public void LoadAd() {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded( string adUnitId ) {
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd() {
        // show the ad:
        Advertisement.Show(_adUnitId, this);
        //Advertisement.Load(_adUnitId, this);
        //GrantReward();
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete( string adUnitId, UnityAdsShowCompletionState showCompletionState ) {
        if( adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) ) {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            GrantReward();
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        }
    }
    public void GrantReward() {

        // Game scene
        if( GameObject.Find("LevelManager") ) {
            int lastUnlockedLv;
            lastUnlockedLv = PlayerPrefs.GetInt("lastUnlockedLv");
            PlayerPrefs.SetInt("lastUnlockedLv", lastUnlockedLv + 4);
            GetComponent<TouchHandler>().LoadNextLevel();
        }
        else {
            int lastUnlockedLv;
            lastUnlockedLv = PlayerPrefs.GetInt("lastUnlockedLv");
            PlayerPrefs.SetInt("lastUnlockedLv", lastUnlockedLv + 4);
            GameObject.Find("LevelTable").GetComponent<LevelTable>().SetLevelTexts();
        }
        
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad( string adUnitId, UnityAdsLoadError error, string message ) {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure( string adUnitId, UnityAdsShowError error, string message ) {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart( string adUnitId ) {
        Debug.Log("Unity Ads Rewarded Ad t1");
    }
    public void OnUnityAdsShowClick( string adUnitId ) {
        Debug.Log("Unity Ads Rewarded Ad t2");
    }

    void OnDestroy() {
        // Clean up the button listeners:
        //_showAdButton.onClick.RemoveAllListeners();
    }
}