
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelData
{
    public int stage;
    public int stars;
    public bool isCleared;
    public bool isPlayable;

    public LevelData(int levelIndex)
    {
        stage = levelIndex;
        stars = 0;
        isCleared = false;
        if (levelIndex == 1) isPlayable = true;
        else isPlayable = false;
    }
}
