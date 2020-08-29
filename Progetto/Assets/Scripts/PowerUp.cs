using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

public class PowerUp : MonoBehaviour
{

    //powerup on/off
    public bool doublejump = false;
    public bool doubledamage = false;
    public bool nodamage = false;

    //tempo di attivazione
    private float dj_activation;
    private float dd_activation;
    private float nd_activation;

    //durata po
    private float dj_time = 30f;
    private float dd_time = 20f;
    private float nd_time = 10f;

    // Start is called before the first frame update
    void Start()
    {
        doublejump = false;
        doubledamage = false;
        nodamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (doublejump && Time.time - dj_activation > dj_time)
            doublejump = false;
        if (doubledamage && Time.time - dd_activation > dd_time)
            doubledamage = false;
        if (nodamage && Time.time - nd_activation > nd_time)
            nodamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            //molla
            case "PU_doublejump":
                doublejump = true;
                dj_activation = Time.time;
                Destroy(other.gameObject);
                break;
            case "PU_doubledamage": //amuchina
                doubledamage = true;
                dd_activation = Time.time;
                Destroy(other.gameObject);
                break;
            case "PU_nodamage":  // mascherina
                nodamage = true;
                dd_activation = Time.time;
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }
}
