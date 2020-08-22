using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTeleport : MonoBehaviour
{

    public GameObject start, check1, check2, check3;
    public Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        switch(PlayerPrefs.GetInt("checkpoint"))
        {
            case 1:
                tr.position = check1.transform.position;
                break;
            case 2:
                tr.position = check2.transform.position;
                break;
            case 3:
                tr.position = check3.transform.position;
                break;
            default:
                tr.position = start.transform.position;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "checkpoint1")
            PlayerPrefs.SetInt("checkpoint", 1);
        if (collision.transform.tag == "checkpoint2")
            PlayerPrefs.SetInt("checkpoint", 2);
        if (collision.transform.tag == "checkpoint3")
            PlayerPrefs.SetInt("checkpoint", 3);
        if (collision.transform.tag == "checkstart")
            PlayerPrefs.SetInt("checkpoint", 0);
    }

}
