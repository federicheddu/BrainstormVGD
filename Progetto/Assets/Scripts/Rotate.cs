using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float speed = 50f;
    bool rotate = false;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.tag == "PU_doublejump" || transform.tag == "PU_doubledamage" || transform.tag == "PU_nodamage" || transform.tag == "pistol" || transform.tag == "assault" || transform.tag == "lmg")
            rotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate)
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    public void SetRotate()
    {
        rotate = true;
    }
}
