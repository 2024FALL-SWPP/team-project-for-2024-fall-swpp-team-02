using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    public Button goBackButton;

    public TextMeshProUGUI level1Text;
    public TextMeshProUGUI level2Text;
    public TextMeshProUGUI level3Text;

    public List<Image> level1Stars;
    public List<Image> level2Stars;
    public List<Image> level3Stars;

    public TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        // Stage 1
        LevelData level1Data = LoadLevelData(1);
        if (level1Data.isPlayable) level1Button.interactable = true;
        if (level1Data.isCleared)
        {
            level1Text.enabled = true;
            for (int i = 0; i < level1Stars.Count; i++)
            {
                if (i < level1Data.stars) level1Stars[i].enabled = true;
            }
        }

        // Stage 2
        LevelData level2Data = LoadLevelData(2);
        if (level2Data.isPlayable) level2Button.interactable = true;
        if (level2Data.isCleared)
        {
            level2Text.enabled = true;
            for (int i = 0; i < level2Stars.Count; i++)
            {
                if (i < level2Data.stars) level2Stars[i].enabled = true;
            }
        }

        // Stage 3
        LevelData level3Data = LoadLevelData(3);
        if (level3Data.isPlayable) level3Button.interactable = true;
        if (level3Data.isCleared)
        {
            level3Text.enabled = true;
            for (int i = 0; i < level3Stars.Count; i++)
            {
                if (i < level3Data.stars) level3Stars[i].enabled = true;
            }
        }
        coinText.text = "  " + DataManager.Instance.GetCoinData().coins;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private LevelData LoadLevelData(int level)
    {
        return DataManager.Instance.GetLevelData(level);
    }

    public void SelectLevel1()
    {
        ActiveLevelData activeLevelData = new ActiveLevelData(1);
        DataManager.Instance.SetActiveLevelData(activeLevelData);
        SceneManager.LoadScene("LevelOneScene");
    }
    public void SelectLevel2()
    {
        ActiveLevelData activeLevelData = new ActiveLevelData(2);
        DataManager.Instance.SetActiveLevelData(activeLevelData);
        SceneManager.LoadScene("LevelTwoScene");
    }
    public void SelectLevel3()
    {
        string link = "https://2024fall-swpp.github.io/team-project-for-2024-fall-swpp-team-02/ending.html?coin=" + DataManager.Instance.GetCoinData().coins;
        Application.OpenURL(link);
        // ActiveLevelData activeLevelData = new ActiveLevelData(3);
        // DataManager.Instance.SetActiveLevelData(activeLevelData);
        // SceneManager.LoadScene("LevelThreeScene");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("StartScene");
    }
}
