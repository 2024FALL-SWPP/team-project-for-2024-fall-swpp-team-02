using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    [SerializeField] private List<float> goalZList = new List<float>() { 24.5f, 24.5f, 24.5f }; //modify after making real level map
    public static StageManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<StageManager>();
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        SceneManager.LoadScene("LevelOverScene");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("LevelClearScene");
    }

    public float GetGoalZ()
    {
        int level = DataManager.Instance.GetActiveLevelData().level;
        return goalZList[level-1];
    }

    public bool IsPaused()
    {
        return Time.timeScale == 0;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
