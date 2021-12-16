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
    // Start is called before the first frame update
    void Start()
    {
        Button leftBtn = transform.GetChild(0).gameObject.GetComponent<Button>();
        Button rightBtn = transform.GetChild(2).gameObject.GetComponent<Button>();

        levelTable = GameObject.Find("LevelTable");
        UpdatePageText();

        leftBtn.onClick.AddListener(LeftOnClick);
        rightBtn.onClick.AddListener(RightOnClick);

        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * width / 10, Screen.safeArea.width * height / 10);
    }

    void LeftOnClick()
    {
        if(levelTable.GetComponent<LevelTable>().page == 1)
        {
            return;
        }
        levelTable.GetComponent<LevelTable>().page = levelTable.GetComponent<LevelTable>().page - 1;
        UpdatePageText();
    }
    void RightOnClick()
    {
        levelTable.GetComponent<LevelTable>().page = levelTable.GetComponent<LevelTable>().page + 1;
        UpdatePageText();
    }
    void UpdatePageText()
    {
        int page;
        page = levelTable.GetComponent<LevelTable>().page;
        string strPage;
        strPage = page.ToString();
        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(strPage);
        levelTable.GetComponent<LevelTable>().SetLevelTexts();
    }

}
