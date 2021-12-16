using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int levelNo;
    public int[] colors;
    public int noOfColors;
    public int variance;
    public string[] winConds;
    public string[] nextWinCons;
    public int[] allowedColors;
    public int changeableColor;
    public int moves;
}
    