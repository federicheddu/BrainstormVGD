using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class MovementPlus : MonoBehaviour
{
    //component
    private CharacterController cc;

    //movimento
    private bool m_crouch, m_crouched;

    //dimensioni
    private Vector3 fullDim;
    private Vector3 crouchDim;

    // Start is called before the first frame update
    void Start()
    {
        //component
        cc = GetComponent<CharacterController>();

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
        }
        else
            transform.localScale = fullDim;
    }

    // Update is called once per frame
    void Update()
    {
        m_crouch = Input.GetKey(KeyCode.C);
    }

}