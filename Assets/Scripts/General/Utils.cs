using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Table;
public class Utils : MonoBehaviour
{
    private float screenAspectRatio;
    private float centerOfSafeArea;
    private float safeAreaHeightInUnityUnits;
    private float safeAreaWidthInUnityUnits;

    public float GetSafeAreaWidth() {
        screenAspectRatio = (float)Screen.width / (float)Screen.height;
        safeAreaWidthInUnityUnits = 10 * screenAspectRatio * (Screen.safeArea.width / Screen.width);
        return safeAreaWidthInUnityUnits;
    }

    public float GetSafeAreaHeight() {
        screenAspectRatio = (float)Screen.width / (float)Screen.height;
        safeAreaHeightInUnityUnits = 10 * Screen.safeArea.height / Screen.height;
        return safeAreaHeightInUnityUnits;
    }
    public float GetCenterOfSafeArea() {
        float bottomHeight = Screen.safeArea.y;
        float topHeight = Screen.height - Screen.safeArea.height - Screen.safeArea.y;
        centerOfSafeArea = ((bottomHeight - topHeight) / 2) * 10 / Screen.height;
        return centerOfSafeArea;
    }

    public float GetCenterOfSafeAreaPixels() {
        float bottomHeight = Screen.safeArea.y;
        float topHeight = Screen.height - Screen.safeArea.height - Screen.safeArea.y;
        centerOfSafeArea = ((bottomHeight - topHeight) / 2);
        return centerOfSafeArea;
    }

}
    