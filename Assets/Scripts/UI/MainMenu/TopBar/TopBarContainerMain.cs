using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarContainerMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width, ( Screen.safeArea.height - Screen.safeArea.width * 7.2f / 10 ) * ( 2f/ 3f ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
