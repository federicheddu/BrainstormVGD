using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoss : MonoBehaviour
{
    bool crown = true;
    float healt = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (healt <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyCrown()
    {
        crown = false;
    }

    public bool CrownState()
    {
        return crown;
    }

    public void TakeDamage(float damage)
    {
        healt -= damage;
    }

}