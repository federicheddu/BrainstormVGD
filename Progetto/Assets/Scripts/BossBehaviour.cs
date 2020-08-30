using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject nem1;
    public GameObject nem2;
    public GameObject crown;
    float healt;
    float ActualHealt;
    Target script;
    bool flag1 = true;
    bool flag2 = true;
    //Transform CrownPosiotion;
    // Start is called before the first frame update
    void Start()
    {
        script = gameObject.transform.GetChild(0).GetComponent<Target>();
        healt = script.maxHealt;
        //CrownPosiotion = gameObject.transform.Find("Crown").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ActualHealt = script.health;
        if(ActualHealt <= (healt / 2) && flag1)
        {
            flag1 = false;
            crown.SetActive(true);
        }
        if (ActualHealt <= (healt / 4) && flag2)
        {
            nem1.SetActive(true);
            nem2.SetActive(true);
            flag2 = false;
        }
    }
}
