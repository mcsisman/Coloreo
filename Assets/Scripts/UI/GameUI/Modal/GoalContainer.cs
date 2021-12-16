using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * 6.4f / 10, Screen.safeArea.width * 2.4f / 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
