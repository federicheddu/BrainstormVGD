using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPu : MonoBehaviour
{
    public GameObject pu;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            timer += Time.deltaTime;
            if (timer >= 30f)
            {
                GameObject clone = Instantiate(pu, transform.position, transform.rotation);
                clone.transform.parent = transform;
                timer = 0;
            }
        }
    }
}
