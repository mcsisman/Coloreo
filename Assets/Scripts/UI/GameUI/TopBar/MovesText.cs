using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static LevelManager;

public class MovesText : MonoBehaviour
{
    public TextMeshProUGUI mt;
    void Start() {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width/4, topBarHeight/5);
        t.anchoredPosition = new Vector3(Screen.safeArea.width / 20, 0, 0);
    }
    public void UpdateMovesText(TextMeshProUGUI moveText) {
        moveText.SetText("Moves\r\n" + GameObject.Find("LevelManager").GetComponent<LevelManager>().moves);
    }
}
