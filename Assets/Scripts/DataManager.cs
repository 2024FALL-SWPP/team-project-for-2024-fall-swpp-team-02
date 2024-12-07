using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//https://gamedevbeginner.com/singletons-in-unity-the-right-way/ 를 참고함
public class DataManager : MonoBehaviour
{

    public static DataManager Instance { get; private set; }
    private List<LevelData> levelDataList = new List<LevelData>();
    private CoinData coinData;
    private ActiveLevelData activeLevelData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        LoadLevelData();
        LoadCoinData();
    }

    private string GetLevelDataPath(int level)
    {
        return Path.Join(Application.persistentDataPath, $"level{level}.json");
    }

    private void LoadLevelData()
    {
        for (int i = 1; i <= 3; i++)
        {
            string path = GetLevelDataPath(i);
            LevelData levelData;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                levelData = JsonUtility.FromJson<LevelData>(json);
            }
            else
            {
                levelData = new LevelData(i);
            }
            levelDataList.Add(levelData);
        }
    }


    public LevelData GetLevelData(int level)
    {
        return levelDataList[level - 1];
    }

    public void SetLevelData(int level, LevelData levelData)
    {
        levelDataList[level - 1] = levelData;
        string path = GetLevelDataPath(level);
        string json = JsonUtility.ToJson(levelData);
        File.WriteAllText(path, json);
    }

    private string GetCoinDataPath()
    {
        return Path.Join(Application.persistentDataPath, "coins.json");
    }
    private void LoadCoinData()
    {
        string path = GetCoinDataPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            coinData = JsonUtility.FromJson<CoinData>(json);
        }
        else
        {
            coinData = new CoinData();
        }
    }

    public CoinData GetCoinData()
    {
        return coinData;
    }

    public void SetCoinData(CoinData coinData)
    {
        this.coinData = coinData;
        string path = GetCoinDataPath();
        string json = JsonUtility.ToJson(coinData);
        File.WriteAllText(path, json);
    }

    public void SetActiveLevelData(ActiveLevelData activeLevelData)
    {
        this.activeLevelData = activeLevelData;
    }

    public ActiveLevelData GetActiveLevelData()
    {
        return activeLevelData;
    }
}
