using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class Actions : MonoBehaviour
{

    private Animator animator;

    public GameObject Player;

    private NavMeshAgent _navMeshAgent;

    public float AttackDistance = 10.0f;
    public float FollowDistance = 12.0f;
    public float AttackProbability = 0.5f;

    public AudioClip GunSound = null;

    const int countOfDamageAnimations = 3;
    int lastDamageAnimation = -1;

    void Awake()
    {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_navMeshAgent.enabled)
        {
            float dist = Vector3.Distance(Player.transform.position, this.transform.position);
            bool shoot = false;
            bool follow = (dist < FollowDistance);

            if (follow)
            {
                float random = Random.Range(0.0f, 1.0f);
                if (random > (1.0f - AttackProbability) && dist < AttackDistance)
                {
                    shoot = true;
                }
            }
            Vector3 mov = Vector3.zero;//
            if (follow)
            {
                _navMeshAgent.SetDestination(mov);// Player.transform.position);
            }

            if (!follow || shoot)
                _navMeshAgent.SetDestination(transform.position);

            if (follow)
            {
                Walk();
            }
            if (shoot)
            {
                Attack();
            }

            //animator.SetBool("Shoot", shoot);
            //animator.SetBool("Walk", follow);

        }
    }


    public void Stay()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 0f);
    }

    public void Walk()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 0.5f);
    }

    public void Run()
    {
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 1f);
    }

    public void Attack()
    {
        Aiming();
        animator.SetTrigger("Attack");
    }

    public void Death()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.Play("Idle", 0);
        else
            animator.SetTrigger("Death");
    }

    public void Damage()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        int id = Random.Range(0, countOfDamageAnimations);
        if (countOfDamageAnimations > 1)
            while (id == lastDamageAnimation)
                id = Random.Range(0, countOfDamageAnimations);
        lastDamageAnimation = id;
        animator.SetInteger("DamageID", id);
        animator.SetTrigger("Damage");
    }

    public void Jump()
    {
        animator.SetBool("Squat", false);
        animator.SetFloat("Speed", 0f);
        animator.SetBool("Aiming", false);
        animator.SetTrigger("Jump");
    }

    public void Aiming()
    {
        animator.SetBool("Squat", false);
        animator.SetFloat("Speed", 0f);
        animator.SetBool("Aiming", true);
    }

    public void Sitting()
    {
        animator.SetBool("Squat", !animator.GetBool("Squat"));
        animator.SetBool("Aiming", false);
    }
}
