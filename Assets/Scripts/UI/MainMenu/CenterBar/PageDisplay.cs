using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageDisplay : MonoBehaviour
{
    [SerializeField] private float width = 4.2f;
    [SerializeField] private float height = 1.4f;
    GameObject levelTable;

    private LevelTable lvlTable;
    // Start is called before the first frame update
    void Start()
    {
        Button leftBtn = transform.GetChild(0).gameObject.GetComponent<Button>();
        Button rightBtn = transform.GetChild(2).gameObject.GetComponent<Button>();

        levelTable = GameObject.Find("LevelTable");
        lvlTable = levelTable.GetComponent<LevelTable>();
        UpdatePageText();

        leftBtn.onClick.AddListener(LeftOnClick);
        rightBtn.onClick.AddListener(RightOnClick);

        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * width / 10, Screen.safeArea.width * height / 10);
    }

    void LeftOnClick()
    {
        if(lvlTable.page == 1){
            return;
        }
        lvlTable.page = lvlTable.page - 1;
        lvlTable.selectedLv = lvlTable.page * 16;
        GameObject.Find("LevelText").transform.GetChild(0).GetComponent<LvTextMain>().UpdateLevelText();
        UpdatePageText();
    }
    void RightOnClick()
    {
        if (lvlTable.lastUnlockedLv <= lvlTable.page * 16){
            return;
        }
        lvlTable.page = lvlTable.page + 1;
        lvlTable.selectedLv = (lvlTable.page-1) * 16 + 1;
        GameObject.Find("LevelText").transform.GetChild(0).GetComponent<LvTextMain>().UpdateLevelText();
        UpdatePageText();
    }
    void UpdatePageText()
    {
        int page = levelTable.GetComponent<LevelTable>().page;
        string strPage = page.ToString();
        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(strPage);
        lvlTable.SetLevelTexts();
    }

}
