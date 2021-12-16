using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinCondText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * (9f / 10f), topBarHeight / 4);
    }

    public void SetWinConditionText(TextMeshProUGUI txt, string[] color) {
        
        string[] winConds = GameObject.Find("LevelManager").GetComponent<LevelManager>().lvl.winConds;
        string display = "";
        for (int i = 0; i < winConds.Length; i++) {
            
            if(i != winConds.Length - 1 ) {
                display = display + ConvertConditionToDisplay(winConds[i]) + "<color=black>" + "     <b>--</b>    " + "</color>";
            }
            else {
                display = display + ConvertConditionToDisplay(winConds[i]);
            }
            
            // At the start all red
            if (color.Length == 0) {
                display = "<color=" + "#EA7272" + ">" + display + "</color>";
            } else {
                display = "<color=" + color[i] + ">" + display + "</color>";
            }
            
        }
        txt.SetText("Achieve\r\n" + display);
    }

    public string ConvertConditionToDisplay(string condition) {

        
        string str1;
        str1 = condition.Replace("xx", "<sprite index= 0>");
        string str2;
        str2 = str1.Replace("yy", "<sprite index= 1>");
        string str3;
        str3 = str2.Replace("zz", "<sprite index= 2>");
        string str4;
        str4 = str3.Replace("tt", "<sprite index= 3>");
        return str4;
    }
}
