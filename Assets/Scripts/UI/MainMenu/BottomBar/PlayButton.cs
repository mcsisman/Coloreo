using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
    private void Start() {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
    }
    void LoadLevel() {
        
        SceneManager.LoadScene("GameScene");
    }
}