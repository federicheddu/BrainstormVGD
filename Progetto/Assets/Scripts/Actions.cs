using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Audio;

[RequireComponent(typeof(Animator))]
public class Actions : MonoBehaviour
{
    private Animator animator;
    GameObject Player;
    public GameObject bullet;
    private NavMeshAgent _navMeshAgent;

    float AttackDistance = 10.0f;
    float FollowDistance = 22.0f;
    private float AttackProbability = 400f;
    float damage = 10f;
    float timer = 0f;
    bool death = false;

    const int countOfDamageAnimations = 3;
    int lastDamageAnimation = -1;


    void Awake()
    {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!death)
        {
            if (_navMeshAgent.enabled)
            {
                float dist = Vector3.Distance(Player.transform.position, this.transform.position);
                bool shoot = dist < AttackDistance;//false;
                bool follow = (dist < FollowDistance);

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
                    timer += Time.deltaTime;
                    Attack();
                    if (timer >= 0.5f)
                    {
                        AudioManager.instance.Play("GunShoot");
                        timer = 0f;
                    }
                }
                if (!follow && !shoot)
                    Stay();
            }
        }else _navMeshAgent.SetDestination(transform.position);
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
        StartCoroutine(Waiter(2f));
        for (int i = 0; i < 30; i++)
        {
            if (Physics.Raycast(bullet.transform.position, 
                new Vector3(bullet.transform.forward.x, bullet.transform.forward.y - i, bullet.transform.forward.z), out hit, 100f))
            {
                Target target = hit.transform.GetComponent<Target>();
                string targetTag = hit.transform.gameObject.tag; //
                if (target != null && targetTag == "Player" && timer >= 0.5f && Random.Range(1, 1000) < AttackProbability) 
                {                    
                    target.TakeDamage(damage);
                    Debug.Log(hit.transform.name + "dal soldato");
                    return;
                }
                
            }
        }
    }


    IEnumerator Waiter(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void Death()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.Play("Idle", 0);
        else
            animator.SetTrigger("Death");
    }

    public void SetDeath()
    {
        death = true;
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
