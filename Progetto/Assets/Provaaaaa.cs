using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provaaaaa : MonoBehaviour
{
    public Target t;
    Coroutine c;
    // Start is called before the first frame update
    void Start()
    {
        t = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
        c = StartCoroutine(C());
        t.health = 1;
    }

    IEnumerator C()
    {
        yield return new WaitForSeconds(1);
        t.health += 1;
        if(t.health >= t.maxHealt)
        {
            StopCoroutine(c);
        }
    }
}
