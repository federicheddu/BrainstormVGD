using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPu : MonoBehaviour
{
    public int type = 0;
    GameObject pu1, pu2, pu3, pu4;
    // Start is called before the first frame update
    void Start()
    {
        pu1 = GameObject.Find("2xjump");
        pu2 = GameObject.Find("2xdamage");
        pu3 = GameObject.Find("NoDamage");
        pu4 = GameObject.Find("RespawnPup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop()
    {
        switch (type)
        {
            case 1:
                Instantiate(pu1, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                break;
            case 2:
                Instantiate(pu2, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                break;
            case 3:
                Instantiate(pu3, transform.position + new Vector3(0, 1f, 0), transform.rotation);
                break;
            case 4:
                Instantiate(pu4, transform.position + new Vector3(0, 1f, 0), transform.rotation);
                break;
            default:
                int doom = Random.Range(1, 100);
                if (doom <= 7)
                {
                    Instantiate(pu1, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                }
                else if (doom <= 14)
                {
                    Instantiate(pu2, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                }
                else if (doom <= 21)
                {
                    Instantiate(pu3, transform.position + new Vector3(0, 1f, 0), transform.rotation);
                }
                else break;
                    
                break;
        }
    }
}
