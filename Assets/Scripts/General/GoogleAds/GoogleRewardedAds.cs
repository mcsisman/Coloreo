using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class GoogleRewardedAds : MonoBehaviour
{

    private RewardedAd rewardedAd;
    [SerializeField] string adUnitAndroid = "ca-app-pub-3940256099942544/5224354917";
    [SerializeField] string adUnitIOS = "ca-app-pub-3940256099942544/1712485313";
    string adUnit;

    void Start() {
        adUnit = ( Application.platform == RuntimePlatform.IPhonePlayer )
            ? adUnitIOS
            : adUnitAndroid;
        this.rewardedAd = new RewardedAd(adUnit);

        MobileAds.Initialize(initStatus => {
            LoadAd();
        });
        
    }

    public void LoadAd() {
        
        adUnit = ( Application.platform == RuntimePlatform.IPhonePlayer )
            ? adUnitIOS
            : adUnitAndroid;
        this.rewardedAd = new RewardedAd(adUnit);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        this.rewardedAd.LoadAd(request);
    }

    public void ShowAd() {
        if( this.rewardedAd.IsLoaded() ) {
            this.rewardedAd.Show();
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

    public void HandleUserEarnedReward( object sender, Reward args ) {
        GrantReward();
        Debug.Log("reward" + args.ToString());
    }
    public void HandleRewardedAdClosed( object sender, EventArgs args ) {
        LoadAd();
        GetComponent<TouchHandler>().RetryLevel();
    }
}
