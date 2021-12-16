using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarContainers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width, topBarHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
