using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallCountText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        float topBarHeight = ( Screen.safeArea.height - Screen.safeArea.width * 8 / 10 ) / 2;
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width*(9f/10f), topBarHeight / 10);
    }

    public void UpdateColorCountText(TextMeshProUGUI txt) {
        LevelManager lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        int[] colorCount = lm.colorCount;
        int[] allowedColors = lm.lvl.allowedColors;
        int changeableColors = lm.lvl.changeableColor;

        string text = "";
        for (int i = 0; i < changeableColors; i++){
            if (i != changeableColors - 1){
                text += (colorCount[allowedColors[i] - 1]) +"  <sprite index= " + (allowedColors[i] - 1) + ">       "; 
            }
            else{
                text +=colorCount[allowedColors[i] - 1] + "  <sprite index= " + (allowedColors[i] - 1) + ">";
            }
        }

        txt.SetText(text);

    }
}
