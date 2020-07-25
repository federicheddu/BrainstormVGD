using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaBullet : MonoBehaviour
{

    public GameObject bullet;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            GameObject bulletObject = Instantiate (bullet);
            bulletObject.transform.position = transform.position;
            bulletObject.transform.forward = transform.forward;

            timer = 0f;
        }
    }
}
