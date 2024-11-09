using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;

    public float speed = 1.0f;


    private GameObject gameManager;

    private float[] offset = { 10.0f, 7.0f, -5.0f };

    private float currentPos = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        currentPos = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos += speed * Time.deltaTime;

        if (player.transform.position.z >= currentPos + 5.0f)
            currentPos += player.transform.position.z - (currentPos + 5.0f);

        transform.position = new Vector3(
            offset[0],
            offset[1],
            currentPos + offset[2]
        );

        if (player.transform.position.z < currentPos - 2)
        {
            player.GetComponent<PlayerBehaviour>().Respawn();
        }
    }
}
