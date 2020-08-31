using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightFinished : MonoBehaviour
{
    public GameObject[] toDisableOnStart;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in toDisableOnStart)
        {
            if(g != null)
            {
                g.SetActive(false);
            }
        }
    }
}
