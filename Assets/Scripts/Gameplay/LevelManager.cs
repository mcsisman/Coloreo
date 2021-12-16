using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using static Levels;
using static LevelText;
using static BallCountText;
using static WinCondText;
using static MovesText;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public GameObject table;
    [SerializeField] private GameObject instructionModal;
    [SerializeField] private GameObject cell;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] public int moves;
    public int currentLevel;
    LevelText lt;
    Levels levels;
    public Level lvl;
    public int[] colorCount;
    private BallCountText ballCountText;
    private WinCondText winCondText;
    public TextMeshProUGUI colorCountUIText;
    public TextMeshProUGUI winCondUIText;
    private MovesText mt;
    public int maxLv;

    // Start is called before the first frame update
    void Start()
    {
        
        winCondText = GameObject.Find("WinCondText").GetComponent<WinCondText>();
        mt = GameObject.Find("MovesText").GetComponent<MovesText>();
        ballCountText = GameObject.Find("BallCountText").GetComponent<BallCountText>();
        lt = GameObject.Find("LevelText").GetComponent<LevelText>();
        
        mt.UpdateMovesText(movesText);

        colorCount = new int[6];
        for(int i = 0; i < 6; i++) {
            colorCount[i] = 0;
        }

        TextAsset myFileData = Resources.Load("levels") as TextAsset;
        
        string myStringData = myFileData.ToString();

        //string json;
        //StreamReader r = new StreamReader("Assets/Scripts/levels.json");    
        //json = r.ReadToEnd();
        
        levels = JsonUtility.FromJson<Levels>(myStringData);
        maxLv = levels.levels.Length;
        
        currentLevel = PlayerPrefs.GetInt("selectedLv");
        LoadLevel(currentLevel);
        
    }

    public void LoadLevel(int level) {
        currentLevel = level;
        for (int i = 0; i < 6; i++) {
            colorCount[i] = 0;
        }
        
        // If the level is 1 and instruction should be shown and moves text circle should be shown
        if (currentLevel == 1 && PlayerPrefs.GetInt("showInstruction", 1) == 1){
            Object.Instantiate(instructionModal, GameObject.Find("Canvas").transform);
            GameObject.Find("MovesText").transform.GetChild(0).gameObject.SetActive(true);
        }
        //Load Next Level
        lvl = levels.levels[currentLevel - 1];
        if (currentLevel < levels.levels.Length)
        {
            lvl.nextWinCons = levels.levels[currentLevel].winConds;
        }
        Table initTable = new Table(8f, table, cell, levels.levels[currentLevel - 1]);
        initTable.CreateTable();
        //Update Level Text

        lt.UpdateLevelText(level, levelText);
        ballCountText.UpdateColorCountText(colorCountUIText);
        string[] tmp = new string[0];
        winCondText.SetWinConditionText(winCondUIText, tmp);
        
        moves = lvl.moves == 0 ? 10 : lvl.moves;
        mt.UpdateMovesText(movesText);
    }
    public void LoadNextLevel() {
        
        for (int i = 0; i < 6; i++) {
            colorCount[i] = 0;
        }
        
        //Load Next Level
        currentLevel++;
        lvl = levels.levels[currentLevel - 1];

        if(currentLevel < levels.levels.Length)
        {
            lvl.nextWinCons = levels.levels[currentLevel].winConds;
        }
        Table initTable = new Table(8f, table, cell, levels.levels[currentLevel - 1]);
        initTable.CreateTable();

        //Update Level Text
        lt.UpdateLevelText(currentLevel, levelText);
        ballCountText.UpdateColorCountText(colorCountUIText);
        string[] tmp = new string[0];
        winCondText.SetWinConditionText(winCondUIText, tmp);
        moves = lvl.moves == 0 ? 10 : lvl.moves;
        mt.UpdateMovesText(movesText);

        //Update highest level
        if (PlayerPrefs.GetInt("highestLv") < currentLevel)
        {
            PlayerPrefs.SetInt("highestLv", currentLevel);
        }
    }
}
