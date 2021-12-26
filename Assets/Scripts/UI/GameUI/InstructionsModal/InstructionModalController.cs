using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionModalController : MonoBehaviour{
    
    
    private int tapCounter = 0;
    
    // Start is called before the first frame update
    void Start(){
        DisableTheScreen();
        CreateTapListener();
    }

    public void DisableTheScreen(){
        GameObject.Find("Initiator").GetComponent<TouchHandler>().touchDisabled = true;
    }
    public void EnableTheScreen(){
        Debug.Log("enabled");
        GameObject.Find("Initiator").GetComponent<TouchHandler>().touchDisabled = false;
    }

    public void CreateTapListener(){
        Button tapListener = GameObject.Find("TapListener").GetComponent<Button>();
        tapListener.onClick.AddListener(OnClickTapButton);
    }

    public void OnClickTapButton(){
        if (tapCounter == 0){
            FirstTap();
        }
        else if (tapCounter == 1){
            SecondTap();
        }
        else if (tapCounter == 2){
            ThirdTap();
        }
        else if (tapCounter == 3){
            FourthTap();
        }
        else if (tapCounter == 4){
            FifthTap();
        }
        else if (tapCounter == 5){
            SixthTap();
        }
    }
    
    public void FirstTap(){
        GameObject.Find("MovesTextCircle").SetActive(false);
        GameObject.Find("QuitButton").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().SetText("You can restart the level or go back to main menu by clicking this button!");
        tapCounter++;
    }
    public void SecondTap(){
        GameObject.Find("QuitButtonCircle").SetActive(false);
        GameObject.Find("BallCountText").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().SetText("You can see the current amount of squares right above the board!");
        tapCounter++;
    }
    public void ThirdTap(){
        GameObject.Find("BallCountTextCircle").SetActive(false);
        GameObject.Find("WinCondText").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().SetText("You can see the objectives you must achieve in order to complete the level in the marked area!");
        tapCounter++;
    }
    public void FourthTap(){
        
        GameObject.Find("WinCondTextCircle").SetActive(false);
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
        string instructionText = "<sprite index=0>  - Shrinks the selection area\r\n" + "<sprite index=2>  - Grows the selection area\r\n" + "<sprite index=1>  - Grants three extra moves";
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().SetText(instructionText);
        tapCounter++;
    }
    public void FifthTap(){
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>().SetText("You are ready! Good luck & Have fun!");
        tapCounter++;
    }
    
    public void SixthTap(){
        
        Destroy(GameObject.Find("InstructionsModal(Clone)"), 0);
        GameObject.Find("Initiator").GetComponent<TouchHandler>().EnableTouchWithDelay();
        tapCounter = 0;
        PlayerPrefs.SetInt("showInstruction", 2);
    }
}
