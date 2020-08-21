using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTellingPlayerController : MonoBehaviour
{
    public Transform targetTransform;
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;
    public float stopDistance = 4f;
    public GameObject myLight;

    private Transform myTransform;
    private Coroutine changeLightCoroutine;
    private bool finished;

    void Awake()
    {
        myTransform = transform;
    }


    void Start()
    {
        myLight.SetActive(true);
        finished = false;
    }

    // https://answers.unity.com/questions/353675/how-to-stop-enemy-within-certain-distance-of-playe.html
    void Update()
    {
        float dist = Vector3.Distance(targetTransform.position, transform.position);
        
        // direzione
        Vector3 targetDir = targetTransform.position - myTransform.position;
        targetDir.y = 0; // per evitare che tilti
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetDir), rotationSpeed * Time.deltaTime);
        if (dist > stopDistance)
        {
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            //changeLightCoroutine = StartCoroutine(ChangeLight());
            finished = true;
            myLight.SetActive(false);
        }
    }


    public bool isInFrontOfTheTable()
    {
        return finished;
    }

}