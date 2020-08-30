using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUIController : MonoBehaviour
{
    public GameObject doubleJump;
    public GameObject noDamage;
    public GameObject doubleDamage;

    private PowerUp pu;

    // Start is called before the first frame update
    void Start()
    {
        pu = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        doubleJump.SetActive(pu.doublejump);
        noDamage.SetActive(pu.nodamage);
        doubleDamage.SetActive(pu.doubledamage);
    }
}
