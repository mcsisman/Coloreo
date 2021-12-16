using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapText : MonoBehaviour
{
    [SerializeField] float width = 7f;
    [SerializeField] float height = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * width / 10, Screen.safeArea.width * height / 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
