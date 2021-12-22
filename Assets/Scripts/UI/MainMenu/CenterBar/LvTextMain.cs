using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvTextMain : MonoBehaviour
{
    public void UpdateLevelText(){
        GetComponent<TextMeshProUGUI>().SetText("Lv. " + GameObject.Find("LevelTable").GetComponent<LevelTable>().selectedLv);
    }
}
