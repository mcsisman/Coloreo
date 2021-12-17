using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesTextContainer : MonoBehaviour
{
    void Start() {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width/4, topBarHeight/5);
        t.anchoredPosition = new Vector3(Screen.safeArea.width / 20, 0, 0);
    }
}
