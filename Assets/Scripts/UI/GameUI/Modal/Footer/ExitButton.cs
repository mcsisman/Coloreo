using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    private void Start() {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ExitToMainMenu);
    }
    void ExitToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
