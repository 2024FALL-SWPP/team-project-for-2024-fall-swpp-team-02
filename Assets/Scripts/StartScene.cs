using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button selectLevelButton;
    public Button powerUpButton;
    void Start()
    {

    }

    void Update()
    {

    }

    public void SelectLevel()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void PowerUp()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("PowerUpManageScene");
    }
}
