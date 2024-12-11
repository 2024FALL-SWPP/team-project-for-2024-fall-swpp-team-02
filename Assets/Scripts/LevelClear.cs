using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelClear : MonoBehaviour
{
    // Start is called before the first frame update
    public Button goBackButton;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public List<Image> starsList;
    private int level;
    void Start()
    {
        ActiveLevelData activeLevelData = DataManager.Instance.GetActiveLevelData();
        int score = activeLevelData.score;
        level = activeLevelData.level;

        int starsCollected = CalculateStars(score);
        int coinsCollected = CalculateCoins(level, starsCollected);

        scoreText.text = "Score: " + score.ToString();
        coinsText.text = "Coins: " + coinsCollected.ToString();

        for (int i = 0; i < starsCollected; i++)
        {
            starsList[i].enabled = true;
        }

        UpdateLevelData(level, starsCollected);
        UpdateCoinData(coinsCollected);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private int CalculateStars(int score)
    {
        if (score >= 90) return 3;
        else if (score >= 80) return 2;
        else if (score >= 70) return 1;
        else return 0;
    }

    private int CalculateCoins(int level, int stars)
    {
        LevelData levelData = DataManager.Instance.GetLevelData(level);
        int coins;
        if (levelData.isCleared)
        {
            int previousStars = levelData.stars;
            coins = Math.Max(0, stars - previousStars);
        }
        else
        {
            coins = stars;
        }

        return coins;
    }

    private void UpdateLevelData(int level, int stars)
    {
        LevelData levelData = DataManager.Instance.GetLevelData(level);
        if (levelData.isCleared)
        {
            levelData.stars = levelData.stars > stars ? levelData.stars : stars;
        }
        else
        {
            levelData.stars = stars;
            levelData.isCleared = true;
        }
        DataManager.Instance.SetLevelData(level, levelData);

        if (level < 3)
        {
            LevelData nextLevelData = DataManager.Instance.GetLevelData(level + 1);
            nextLevelData.isPlayable = true;
            DataManager.Instance.SetLevelData(level + 1, nextLevelData);
        }
    }

    private void UpdateCoinData(int coinsCollected)
    {
        CoinData coinData = DataManager.Instance.GetCoinData();
        coinData.coins += coinsCollected;
        DataManager.Instance.SetCoinData(coinData);
    }

    public void GoBack()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void Restart()
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("LevelOneScene");
                break;
            case 2:
                SceneManager.LoadScene("LevelTwoScene");
                break;
            case 3:
                SceneManager.LoadScene("LevelThreeScene");
                break;
        }

        AudioManager.Instance.PlaySFX("ButtonClick");

    }
}
