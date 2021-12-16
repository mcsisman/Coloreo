using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class SafeArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
        t.anchoredPosition = new Vector3(0, Screen.safeArea.y, 0);
    }
    
}
