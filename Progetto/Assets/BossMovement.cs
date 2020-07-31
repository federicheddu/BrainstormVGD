using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform[] patrolPoints;
    private int currentControlPointIndex = 0;
    GameObject player;
    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {//rotazione costante
        

        
        if (start)
        {
            MoveToNextPatrolPoint();
            start = false;
        }
        else
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.2f && start == false)
        {
            MoveToNextPatrolPoint();
            Debug.Log("nav mesh.pathpending e distanza rimane");
        }
        

        
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            _navMeshAgent.destination = patrolPoints[currentControlPointIndex].position;

            currentControlPointIndex++;
            currentControlPointIndex %= patrolPoints.Length;
        }
        else Debug.Log("Lista vuota");
    }
}
