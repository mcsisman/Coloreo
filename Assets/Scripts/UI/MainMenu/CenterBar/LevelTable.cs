using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelTable : MonoBehaviour
{
    [SerializeField] public int page;
    [SerializeField] public int selectedLv;
    [SerializeField] public int lastUnlockedLv;
    [SerializeField] private GameObject unlockLevelModal;
    
    [SerializeField] private float width = 5.1f;
    [SerializeField] private float height = 5.1f;
    public int highestLv;
    // Start is called before the first frame update
    void Start(){
        // Delete all player prefs
        //PlayerPrefs.DeleteAll();
        
        highestLv = PlayerPrefs.GetInt("highestLv", 1);
        lastUnlockedLv = PlayerPrefs.GetInt("lastUnlockedLv", 4);
        PlayerPrefs.SetInt("lastUnlockedLv", lastUnlockedLv);
        selectedLv = highestLv;
        PlayerPrefs.SetInt("selectedLv", selectedLv);
        page = ((selectedLv -1) / 16) + 1;
        //SetLevelTexts();

        GameObject.Find("LevelText").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Lv. " + selectedLv);
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(Screen.safeArea.width * width / 10, Screen.safeArea.width * height / 10);
    }

    [SerializeField]
    public void SetLevelTexts()
    {
        highestLv = PlayerPrefs.GetInt("highestLv");
        lastUnlockedLv = PlayerPrefs.GetInt("lastUnlockedLv");
        for (int i = 0; i < 4; i++){
            for (int j = 0; j < 4; j++) {
                GameObject child = this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).gameObject;
                
                int level;
                level = (page-1) * 16 + (i *4) + (j + 1);

                // Low opacity levels
                if (level > highestLv){
                    Image img;
                    child.GetComponent<Button>().interactable = false;
                    img = child.transform.GetChild(0).gameObject.GetComponent<Image>();
                    var tempColor = img.color;
                    tempColor.a = .4f;
                    img.color = tempColor;
                }
                else {
                    Image img;
                    child.GetComponent<Button>().interactable = true;
                    img = child.transform.GetChild(0).gameObject.GetComponent<Image>();
                    var tempColor = img.color;
                    tempColor.a = 1f;
                    img.color = tempColor;
                }
                // TextMeshPro object of the empty circle
                GameObject empty = child.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
                // TextMeshPro object of the filled circle
                GameObject filled = child.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                // Fill the selected level circle
                if (level == selectedLv){
                    filled.transform.parent.gameObject.SetActive(true);
                }
                else{
                    filled.transform.parent.gameObject.SetActive(false);
                }
                // Show locked icon on locked levels
                GameObject lockObj = child.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                Button lockBtn = lockObj.transform.GetChild(0).gameObject.GetComponent<Button>();
                if( level > lastUnlockedLv ) {

                    lockBtn.interactable = true;
                    lockObj.SetActive(true);
                    lockBtn.onClick.RemoveAllListeners();
                    lockBtn.onClick.AddListener(LockBtnOnClick);

                    // Deactivate the level text
                    empty.SetActive(false);
                }
                else {
                    lockBtn.interactable = false;
                    lockObj.SetActive(false);

                    // Activate the level text
                    empty.SetActive(true);
                }
                string result;
                result = level.ToString();
                // Write the level texts in unlocked levels
                empty.GetComponent<TextMeshProUGUI>().SetText(result);
                filled.GetComponent<TextMeshProUGUI>().SetText(result);

            }
        }
    }

    void LockBtnOnClick() {
        //Currently disabled!
        //Object.Instantiate(unlockLevelModal, GameObject.Find("Canvas").GetComponent<Transform>());
    }

}
