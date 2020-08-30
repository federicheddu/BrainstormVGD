using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBoss : MonoBehaviour
{
    public GameObject Boss2;
    float healt;
    float ActualHealt;
    Target script;
    bool flag1 = true;
    // Start is called before the first frame update
    void Start()
    {
        script = gameObject.transform.GetChild(0).GetComponent<Target>();
        healt = script.maxHealt;
    }

    // Update is called once per frame
    void Update()
    {
        ActualHealt = script.health;
        if (ActualHealt <= (healt / 2) && flag1)
        {
            flag1 = false;
            Boss2.SetActive(true);
        }
    }
}
