using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        RectTransform tParent = transform.parent.gameObject.GetComponent<RectTransform>();
        tParent.sizeDelta = new Vector2(Screen.safeArea.width * 6.4f / 10, Screen.safeArea.width * 3.2f / 10);
        tParent.anchoredPosition = new Vector2(0, Screen.safeArea.width * 3.2f / 10);
        
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * 2.4f / 10, Screen.safeArea.width * 2.4f / 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}