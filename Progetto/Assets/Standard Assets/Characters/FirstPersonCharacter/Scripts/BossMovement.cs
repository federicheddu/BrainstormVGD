using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform[] patrolPoints;
    public int currentControlPointIndex = 0;
    GameObject player;
    bool start = true;
    float AttackDistance = 100f;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //controlla se il nemico è abbastanza vicino e poi inizia a muoversi con la partol
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist < AttackDistance)
        {
            if (start)
            {
                MoveToNextPatrolPoint();
                start = false;
                transform.GetChild(0).GetComponent<Rotate>().SetRotate();
            }
            else
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.2f && start == false)
            {

                MoveToNextPatrolPoint();
            }
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
