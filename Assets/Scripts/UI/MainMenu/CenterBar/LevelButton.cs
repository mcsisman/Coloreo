using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        
    }

    void OnClick(){
        // Get the clicked level number
        int column = transform.GetSiblingIndex();
        int row = transform.parent.gameObject.transform.GetSiblingIndex();

        // Fill the button
        GameObject filled = this.gameObject.transform.GetChild(1).gameObject;
        filled.SetActive(true);
        

        // Empty the previous button 
        GameObject levelTable = GameObject.Find("LevelTable");
        
        int previousLv = levelTable.GetComponent<LevelTable>().selectedLv;
        int previousPage = levelTable.GetComponent<LevelTable>().page;
        previousLv = previousLv - (previousPage-1) * 16;

        int previousRow = (int)((previousLv-1) / 4);
        int previousColumn = (previousLv-1) % 4;

        if (previousRow == row & previousColumn == column)
        {
            return;
        }

        // Set the selectedLv
        int page = levelTable.GetComponent<LevelTable>().page;
        int selectedLv = (page - 1) * 16 + (row * 4) + (column + 1);
        PlayerPrefs.SetInt("selectedLv", selectedLv);
        levelTable.GetComponent<LevelTable>().selectedLv = selectedLv;
        
        // Set the level text
        GameObject.Find("LevelText").transform.GetChild(0).GetComponent<LvTextMain>().UpdateLevelText();
        // Remove the previous selection
        levelTable.transform.GetChild(previousRow).gameObject.transform.GetChild(previousColumn).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        
    }


}
