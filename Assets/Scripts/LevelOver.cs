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
        SceneManager.LoadScene("Level" + level.ToString());
    }
}
