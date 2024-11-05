using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectStage : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    // Start is called before the first frame update
    void Start()
    {
        int highestStageCleared = PlayerPrefs.GetInt("highestStageCleared", 0);
        if (highestStageCleared >= 0) stage1Button.interactable = true;
        if (highestStageCleared >= 1) stage2Button.interactable = true;
        if (highestStageCleared >= 2) stage3Button.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
