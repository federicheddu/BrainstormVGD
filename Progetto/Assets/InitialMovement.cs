using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;
    public float stopDistance = 4f;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // https://answers.unity.com/questions/353675/how-to-stop-enemy-within-certain-distance-of-playe.html
    void Update()
    {


        Debug.DrawLine(playerTransform.position, myTransform.position, Color.red);
        float dist = Vector3.Distance(playerTransform.position, transform.position);
        
        // get the target direction:
        Vector3 targetDir = playerTransform.position - myTransform.position;
        targetDir.y = 0; // kill any height difference to avoid tilting
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetDir), rotationSpeed * Time.deltaTime);
        if (dist > stopDistance)
        {
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Replace with code to do when character stops
        }
        
    }

}