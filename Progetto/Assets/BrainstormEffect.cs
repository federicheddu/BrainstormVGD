using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainstormEffect : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;
    private Transform myTransform;
    private Transform targetTransform;
    private bool finished;



    void Awake()
    {
        myTransform = gameObject.transform;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player_Brainstorm_Target");
        targetTransform = target.transform;
        finished = false;
        StartCoroutine(StopBrainstorm());
    }

    public bool hasFinished() { return finished; }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);        
    }

    IEnumerator StopBrainstorm()
    {
        yield return new WaitForSeconds(3f);
        finished = true;
    }
}