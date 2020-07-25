using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkBoss : MonoBehaviour
{
    public TargetBoss targetBoss;
    float healt;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Crown")
        {
            healt = 50;
        }
        else healt = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (healt <= 0)
        {
            if (gameObject.name == "Crown")
            {
                targetBoss.DestroyCrown();
                Destroy(gameObject);
            }
            else
            {
                targetBoss.TakeDamage(20);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (gameObject.name != "Crown" && !targetBoss.CrownState())
        {
            healt -= damage;
        }
        else if (gameObject.name == "Crown")
        {
            healt -= damage;
        }
        else return;

    }
}
