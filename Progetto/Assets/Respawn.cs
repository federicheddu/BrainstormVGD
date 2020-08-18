﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    GameObject player;
        GameObject respwanPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        respwanPoint = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = respwanPoint.transform.position;
        player.GetComponent<Rigidbody>().velocity = new Vector3( 0, 0, 0);
    }
}
