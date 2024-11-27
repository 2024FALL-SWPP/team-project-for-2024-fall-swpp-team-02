using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    [SerializeField] private float goalZ;

    public static StageManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<StageManager>();
            return instance;
        }
    }


    public PlayerBagController bagController;


    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bagController = new PlayerBagController(6);
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
        return goalZ;
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
