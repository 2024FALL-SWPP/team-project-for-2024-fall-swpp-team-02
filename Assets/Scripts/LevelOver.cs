using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOver : MonoBehaviour
{
    public Button goBackButton;
    public Button restartButton;

    private int level;
    // Start is called before the first frame update
    void Start()
    {
        level = DataManager.Instance.GetActiveLevelData().level;
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
    }
}
