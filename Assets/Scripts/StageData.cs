
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageData
{
    public int stage;
    public int stars;
    public bool isCleared;
    public bool isPlayable;

    public StageData(int stageIndex)
    {
        stage = stageIndex;
        stars = 0;
        isCleared = false;
        if (stageIndex == 1) isPlayable = true;
        else isPlayable = false;
    }
}
