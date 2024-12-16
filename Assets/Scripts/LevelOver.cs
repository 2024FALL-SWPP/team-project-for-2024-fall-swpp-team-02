using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOver : MonoBehaviour
{
    public Button goBackButton;
    public Button restartButton;
    public GameObject robot1;
    public GameObject robot2;
    public float rotationSpeed = 15f;
    public float rotationRange = 30f;

    private float initialZRotation1;
    private float initialZRotation2;

    private int level;
    // Start is called before the first frame update
    void Start()
    {
        level = DataManager.Instance.GetActiveLevelData().level;
        if (robot1 != null)
            initialZRotation1 = robot1.transform.eulerAngles.z;

        if (robot2 != null)
            initialZRotation2 = robot2.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        RotateRobotBackAndForth(robot1, initialZRotation1);
        RotateRobotBackAndForth(robot2, initialZRotation2);
    }
    private void RotateRobotBackAndForth(GameObject robot, float initialZRotation)
    {
        if (robot != null)
        {
            float zRotationOffset = Mathf.PingPong(Time.time * rotationSpeed, rotationRange * 2) - rotationRange;

            Vector3 currentRotation = robot.transform.eulerAngles;
            robot.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, initialZRotation + zRotationOffset);
        }
    }

    public void GoBack()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
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
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
}
