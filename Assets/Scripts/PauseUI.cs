using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (StageManager.Instance.IsPaused()) StageManager.Instance.ResumeGame();
        else StageManager.Instance.PauseGame();
    }
}
