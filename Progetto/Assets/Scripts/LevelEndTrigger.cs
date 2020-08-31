using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject[] toActivateOnEnd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            foreach(GameObject g in toActivateOnEnd)
            {
                if(g != null)
                {
                    g.SetActive(true);
                }
            }
        }
    }
}
