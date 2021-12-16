using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapImage : MonoBehaviour
{
    [SerializeField] float width = 1.8f;
    [SerializeField] float height = 1.8f;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * width / 10, Screen.safeArea.width * height / 10);
    }

}
