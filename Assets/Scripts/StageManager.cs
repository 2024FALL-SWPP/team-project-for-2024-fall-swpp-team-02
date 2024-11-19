using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
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
}
