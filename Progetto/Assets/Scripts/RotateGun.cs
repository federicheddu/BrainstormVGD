using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public GrapplingGun grappling;
    public GameObject pistol, assault, lmg;
    private GrapplingGun grapPistol, grapAss, grapLmg;
    public float rotationSpeed = 5f;

    private Quaternion desideredRotation;

    // Start is called before the first frame update
    void Start()
    {
        grapPistol = pistol.GetComponent<GrapplingGun>();
        grapAss = assault.GetComponent<GrapplingGun>();
        grapLmg = lmg.GetComponent<GrapplingGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pistol.activeInHierarchy)
            grappling = grapPistol;
        if (assault.activeInHierarchy)
            grappling = grapAss;
        if (lmg.activeInHierarchy)
            grappling = grapLmg;

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
