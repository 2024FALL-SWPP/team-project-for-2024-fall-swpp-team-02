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

    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
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
        return goalZ;
    }
}
