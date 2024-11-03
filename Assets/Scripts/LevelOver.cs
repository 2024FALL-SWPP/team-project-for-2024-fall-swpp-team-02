using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOver : MonoBehaviour
{
    public Button goBackButton;
    public Button restartButton;

    private int stage;
    // Start is called before the first frame update
    void Start()
    {
        stage = PlayerPrefs.GetInt("Stage", 0);
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
        SceneManager.LoadScene("Stage" + stage.ToString());
    }
}
