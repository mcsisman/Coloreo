using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static LevelManager;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI lt;
    void Start() {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width / 4, topBarHeight / 5);
    }
    public void UpdateLevelText(int level, TextMeshProUGUI levelText) {
        levelText.SetText("Level\r\n" + level);
        
    }
}
