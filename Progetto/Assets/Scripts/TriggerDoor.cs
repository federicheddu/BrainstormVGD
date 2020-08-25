using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    bool flag = false;
    float timer = 0f;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            timer += Time.deltaTime;
            if (timer >= 30f)
            {
                //parte il dialogo
                door.GetComponent<SunTemple.Door>().enabled = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            flag = true;
        }
    }
}
