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
