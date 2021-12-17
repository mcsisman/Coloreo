using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using static Level;

public class Table{
    private float noOfRows;
    private GameObject cell;
    private GameObject table;
    private float safeAreaHeight;
    private float safeAreaWidth;
    private float centerOfSafeArea;
    private GameObject tableObj;
    private int[] colorEnumArray;
    private Level level;
    public int[] colorCount;

    public Table() {
    }
    public Table(float noOfRows, GameObject table, GameObject cell, Level level) {
        this.noOfRows = noOfRows;
        this.table = table;
        this.cell = cell;
        this.level = level;
        colorCount = new int[6];
    }
    public void CreateTable(){
        Utils util = GameObject.Find("Initiator").GetComponent<Utils>();
        safeAreaHeight = util.GetSafeAreaHeight();
        safeAreaWidth = util.GetSafeAreaWidth();
        centerOfSafeArea = util.GetCenterOfSafeArea();

        table.transform.localScale = new Vector3(safeAreaWidth*8/10, safeAreaWidth*8/10, 1);
        table.transform.position = new Vector3(0, centerOfSafeArea, 0);

        tableObj = Object.Instantiate(table);
        createCells();
    }

    public void DecreaseCountOfIndex(int index) {
        colorCount[index] = colorCount[index] - 1;
    }

    public void IncreaseCountOfIndex(int index) {
        colorCount[index] = colorCount[index] + 1;
    }


    public void createCells() {
        ArrayList colorList = CreateCellColorArray();
        colorList = Shuffle(colorList);

        float cellSize = .9f / noOfRows;
        GameObject[,] cells = new GameObject[(int)noOfRows, (int)noOfRows];

        float positionX = -.45f + cellSize/2;
        float positionY = .45f - cellSize/2;

        for (int i = 0; i < noOfRows; i++) {
            for (int j = 0; j < noOfRows; j++) {
                cells[i, j] = cell;
                cells[i, j].transform.localScale = new Vector3(cellSize, cellSize, 1);
                cells[i, j].transform.position = new Vector3(positionX, positionY, -1);
                GameObject circle = cells[i, j].transform.GetChild(0).gameObject;
                circle.GetComponent<SpriteRenderer>().color = GetColorOfIndex((int)colorList[i*8 + j]);
                
                //increase color count of corresponding type
                GameObject.Find("LevelManager").GetComponent<LevelManager>().colorCount[(int)colorList[i * 8 + j] - 1] = GameObject.Find("LevelManager").GetComponent<LevelManager>().colorCount[(int)colorList[i * 8 + j] - 1] + 1;

                cells[i, j].GetComponent<BallInformation>().row = i;
                cells[i, j].GetComponent<BallInformation>().column = j;
                int colorEnum = (int)colorList[i * 8 + j] - 1;
                cells[i, j].GetComponent<BallInformation>().colorEnum = colorEnum;
                cells[i, j].name = "cell" + i + "," + j;

                
                if(colorEnum < 4)
                {
                    circle.GetComponent<SpriteRenderer>().sprite = circle.GetComponent<BallInformation>().circleSprite;
                }
                if (colorEnum == 4)
                {
                    circle.GetComponent<SpriteRenderer>().sprite = circle.GetComponent<BallInformation>().deathSprite;
                }
                

                Object.Instantiate(cells[i, j], tableObj.transform);
                positionX = positionX + cellSize;
            }
            positionX = -.45f + cellSize/2;
            positionY = positionY - cellSize;
        }
    }

    public ArrayList CreateCellColorArray() {

        int variance = level.variance;
        int noOfColors = level.noOfColors;
        int[] tmp = new int[level.changeableColor];
        int[] colorsArray = new int[noOfColors];
        int[] allowedColors = level.allowedColors;

        // Store changeable colors on a tmp array to apply variance
        for (int i = 0; i < level.changeableColor; i++){
            tmp[i] = level.colors[allowedColors[i] - 1];
        }
        // Apply variance to colored cells only
        tmp = ApplyVarianceToColorNumbers(tmp, variance);
        // Save the colored cell amounts
        for(int i = 0; i < level.changeableColor; i++ ) {
            colorsArray[i] = tmp[i];
        }
        // Get the variance unapplied cells and merge
        for(int i = level.changeableColor; i < noOfColors; i++ ) {
            colorsArray[i] = level.colors[allowedColors[i] - 1];
        }
        ArrayList temp = new ArrayList();
        for(int i = 0; i < noOfColors; i++) {
            for(int j = 0; j < colorsArray[i]; j++) {
                temp.Add(allowedColors[i]);
            }
        }
        return temp;
    }

    public int[] ApplyVarianceToColorNumbers(int[] colorArray, int variance) {
        ArrayList randomList = new ArrayList();
        int[] randomArray = new int[colorArray.Length+1];
        randomList.Add(0);
        
        for (int i = 1; i < colorArray.Length; i++) {
            randomList.Add(Random.Range(0, (variance * colorArray.Length) + 1));
        }
        randomList.Add(variance * colorArray.Length);
        randomList.Sort();
        for (int i = 0; i < colorArray.Length; i++) {
            colorArray[i] = colorArray[i] - variance + (int)randomList[i+1] - (int)randomList[i];
        }
        return colorArray;
    }

    public Color GetColorOfIndex(int index) {
        Color[] colorArr = new Color[6];
        colorArr[0] = new Color(.922f, .541f, .541f, 1);
        colorArr[1] = new Color(.557f, .549f, 1, 1);
        colorArr[2] = new Color(.702f, 1f, .824f, 1);
        colorArr[3] = new Color(.988f, .988f, .663f, 1);
        colorArr[4] = new Color(1f, 1f, 1f, 1f);
        colorArr[5] = new Color(0f, 0f, 0f, 1f);

        return colorArr[index-1];
    }

    public Color GetRandomSpriteColor(int noOfColors) {
        Color[] colorArr = new Color[6];
        colorArr[0] = new Color(.922f, .541f, .541f, 1);
        colorArr[1] = new Color(.557f, .549f, 1, 1);
        colorArr[2] = new Color(.702f, 1f, .824f, 1);
        colorArr[3] = new Color(.988f, .988f, .663f, 1);
        colorArr[4] = new Color(1f, 1f, 1f, 1f);
        colorArr[5] = new Color(0f, 0f, 0f, 1f);
        int randomIndex = Random.Range(0, noOfColors);
        return colorArr[randomIndex];
    }

    public ArrayList Shuffle(ArrayList list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n);
            int value = (int)list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}
