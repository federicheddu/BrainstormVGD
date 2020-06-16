using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public GrapplingGun grappling;
    public float rotationSpeed = 5f;

    private Quaternion desideredRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!grappling.IsGrappling())
        {
            desideredRotation = transform.parent.rotation;
        }
        else
        {
            desideredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, desideredRotation, Time.deltaTime * rotationSpeed);
    }
}
