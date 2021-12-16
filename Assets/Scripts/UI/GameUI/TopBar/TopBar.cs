using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Utils;
public class TopBar : MonoBehaviour
{
    public Image topBar;
    // Start is called before the first frame update
    void Start()
    {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width, topBarHeight);
        Utils u = GameObject.Find("Initiator").GetComponent<Utils>();
        t.localPosition = new Vector3(0, u.GetCenterOfSafeAreaPixels() + topBarHeight / 2 + (Screen.safeArea.width*8/10)/2, 0); 
    }

}
