using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public TextMeshProUGUI stage1Text;
    public TextMeshProUGUI stage2Text;
    public TextMeshProUGUI stage3Text;

    public List<Image> stage1Stars;
    public List<Image> stage2Stars;
    public List<Image> stage3Stars;
    // Start is called before the first frame update
    void Start()
    {
        // Stage 1
        StageData stage1Data = LoadLevelData(1);
        if (stage1Data.isPlayable) stage1Button.interactable = true;
        if (stage1Data.isCleared)
        {
            stage1Text.enabled = true;
            for (int i = 0; i < stage1Stars.Count; i++)
            {
                if (i < stage1Data.stars) stage1Stars[i].enabled = true;
            }
        }

        // Stage 2
        StageData stage2Data = LoadLevelData(2);
        if (stage2Data.isPlayable) stage2Button.interactable = true;
        if (stage2Data.isCleared)
        {
            stage2Text.enabled = true;
            for (int i = 0; i < stage2Stars.Count; i++)
            {
                if (i < stage2Data.stars) stage2Stars[i].enabled = true;
            }
        }

        // Stage 3
        StageData stage3Data = LoadLevelData(3);
        if (stage3Data.isPlayable) stage3Button.interactable = true;
        if (stage3Data.isCleared)
        {
            stage3Text.enabled = true;
            for (int i = 0; i < stage3Stars.Count; i++)
            {
                if (i < stage3Data.stars) stage3Stars[i].enabled = true;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private StageData LoadLevelData(int stage)
    {
        if (PlayerPrefs.HasKey("StageData" + stage.ToString()))
        {
            string jsonData = PlayerPrefs.GetString("StageData" + stage.ToString());
            return JsonUtility.FromJson<StageData>(jsonData);
        }

        return new StageData(stage);
    }

    public void SelectStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void SelectStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void SelectStage3()
    {
        SceneManager.LoadScene("Stage3");
    }
}
