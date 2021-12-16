using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Divider : MonoBehaviour
{
    public Image divider;
    public float DividerLength = 8f;
    public float DividerHeight = 6f;
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width/10*DividerLength, DividerHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
