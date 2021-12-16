using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapContainer : MonoBehaviour
{
    [SerializeField] float width = 10f;
    [SerializeField] float height = 3.3f;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * width / 10, Screen.safeArea.width * height / 10);
        t.anchoredPosition = new Vector3(0, Screen.safeArea.y + 20, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
