using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Audio;

[RequireComponent(typeof(Animator))]
public class Actions : MonoBehaviour
{

    private Animator animator;

    public GameObject Player;
    public GameObject bullet;

    private NavMeshAgent _navMeshAgent;

    public float AttackDistance = 10.0f;
    public float FollowDistance = 22.0f;
    public float AttackProbability = 0f;
    public float damage = 10f;
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
            bool shoot = dist < AttackDistance;//false;
            bool follow = (dist < FollowDistance);
            /*
            if (follow)
            {
                float random = Random.Range(0.0f, 1.0f);
                if (random > (1.0f - AttackProbability) && dist < AttackDistance)
                {
                    shoot = true;
                }
            }
            */
            if (follow)
            {
                _navMeshAgent.SetDestination(Player.transform.position);
            }

            if (!follow || shoot)
                _navMeshAgent.SetDestination(transform.position);

            if (follow)
            {
                FaceTarget();
                Walk();
            }
            if (shoot)
            {
                FaceTarget();
                Attack();
            }
            if (!follow && !shoot)
                Stay();

            //animator.SetBool("Shoot", shoot);
            //animator.SetBool("Walk", follow);

        }
    }


    void FaceTarget()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        Quaternion lookRotation =
            Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation =
            Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
        animator.SetFloat("Speed", 1f);//1
    }

    public void Attack()
    {
        Aiming();
        animator.SetTrigger("Attack");
        // metti un figlio e usa get component
        RaycastHit hit;
        //Ray MyRay = new Ray(bullet.transform.position, Vector3.forward);
        StartCoroutine(Waiter());
        if(Physics.Raycast(bullet.transform.position, bullet.transform.forward, out hit, 100f))//Vector3.forward, out hit, 100f))
        {
            if(Random.Range(1,1000) < AttackProbability){
                Debug.Log(hit.transform.name);
                Debug.DrawRay(bullet.transform.position, Vector3.forward * 100f, Color.green);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                    target.TakeDamage(damage);
            }
        }
        
        /*
        if (m_Audio != null)
        {
            m_Audio.PlayOneShot(GunSound);
        }
        */
    }


    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
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
