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
        level = PlayerPrefs.GetInt("level");
        int score = PlayerPrefs.GetInt("score", 0);
        int stars = PlayerPrefs.GetInt("stars", 0);
        int coinsCollected = PlayerPrefs.GetInt("coinsCollected", 0);
        scoreText.text = "Score: " + score.ToString();
        coinsText.text = "Coins: " + coinsCollected.ToString();
        for (int i = 0; i < starsList.Count; i++)
        {
            if (i < stars) starsList[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }
}
