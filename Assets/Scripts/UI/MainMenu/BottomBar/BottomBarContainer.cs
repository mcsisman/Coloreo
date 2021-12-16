using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width, ( Screen.safeArea.height - Screen.safeArea.width * 8.1f / 10 ) * ( 1f / 3f ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
