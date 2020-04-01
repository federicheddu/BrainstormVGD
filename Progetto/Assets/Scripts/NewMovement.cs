using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class NewMovement : MonoBehaviour
{
    //component
    private Rigidbody rb;

    //movimento
    private bool m_crouch, m_crouched;

    //dimensioni
    private Vector3 fullDim;
    private Vector3 crouchDim;

    // Start is called before the first frame update
    void Start()
    {
        //component
        rb = GetComponent<Rigidbody>();

        //input
        m_crouch = false;

        //dimensioni
        fullDim = transform.localScale;
        crouchDim = new Vector3(fullDim.x, fullDim.y / 2, fullDim.z);
    }

    private void FixedUpdate()
    {

        if (m_crouch)
        {
            transform.localScale = crouchDim;
            rb.AddForce(new Vector3(0f, 100f, 115f), ForceMode.Impulse);
        } else
            transform.localScale = fullDim;
    }

    // Update is called once per frame
    void Update()
    {
        m_crouch = Input.GetKey(KeyCode.LeftControl);
    }

    void OnControl

}