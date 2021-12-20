using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width / 12, Screen.safeArea.width / 12);
        t.anchoredPosition = new Vector3(-Screen.safeArea.width / 9, 0, 0);

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ExitToMainMenu);
    }

    void ExitToMainMenu()
    {
        GameObject.Find("Initiator").GetComponent<TouchHandler>().LevelFailed();
        //SceneManager.LoadScene("MainMenu");
    }
}
