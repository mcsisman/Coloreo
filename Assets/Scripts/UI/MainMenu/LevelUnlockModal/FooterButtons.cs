using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FooterButtons : MonoBehaviour
{
    private Button exitButton;
    private Button adButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton = transform.GetChild(0).gameObject.GetComponent<Button>();
        exitButton.onClick.AddListener(ExitBtnOnClick);
        adButton = transform.GetChild(1).gameObject.GetComponent<Button>();
        adButton.onClick.AddListener(RunRewardedAd);
    }

    void ExitBtnOnClick() {
        if( GameObject.Find("LevelManager") ) {
            SceneManager.LoadScene("MainMenu");
        }
        else {
            DestroyModal();
        }
    }
    void DestroyModal() {
        exitButton.interactable = false;
        exitButton.onClick.RemoveAllListeners();
        adButton.onClick.RemoveAllListeners();
        Destroy(GameObject.Find("UnlockLevelsModal(Clone)"), 0);

    }
    void RunRewardedAd() {
        adButton.interactable = false;
        DestroyModal();
        if( GameObject.Find("LevelManager") ) {
            GameObject.Find("Initiator").GetComponent<GoogleRewardedAds>().ShowAd();
        }
        else {
            // Currently disabled!
            GameObject.Find("SafeAreaContainer").GetComponent<GoogleRewardedAds>().ShowAd();
        }
        
    }

    
}
