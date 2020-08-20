using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventola : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed1 * Time.deltaTime, Space.Self);
    }
}
