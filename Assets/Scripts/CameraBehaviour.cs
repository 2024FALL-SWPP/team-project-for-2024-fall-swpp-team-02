using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;

    public float speed = 1.0f;

    public float maxOffset = 5.0f;
    public float minOffset = -2.0f;


    private Vector3 offset = new Vector3(10.0f, 7.0f, -5.0f);

    private float currentPos = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        currentPos = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = Mathf.Max(currentPos + speed * Time.deltaTime, player.transform.position.z - maxOffset);

        transform.position = offset + new Vector3(0, 0, currentPos);

        if (player.transform.position.z < currentPos + minOffset)
        {
            player.GetComponent<PlayerBehaviour>().Respawn();
        }
    }
}
