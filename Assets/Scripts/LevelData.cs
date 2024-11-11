
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelData
{
    public int level;
    public int stars;
    public bool isCleared;
    public bool isPlayable;

    public LevelData(int levelIndex)
    {
        level = levelIndex;
        stars = 0;
        isCleared = false;
        isPlayable = levelIndex == 1;
    }
}
