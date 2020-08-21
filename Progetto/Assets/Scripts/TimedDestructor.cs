using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestructor : MonoBehaviour
{
    public float delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimedDestroy());
    }

    IEnumerator TimedDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
