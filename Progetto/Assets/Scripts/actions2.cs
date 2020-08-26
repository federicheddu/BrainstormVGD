using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Audio;

public class actions2 : MonoBehaviour
{

    GameObject Player;
    public GameObject bullet;
    //private Animator animator;
    private NavMeshAgent _navMeshAgent;

    float AttackDistance = 30f;
    float FollowDistance = 40f;
    private float AttackProbability = 250f;
    public float damage = 10f;
    public ParticleSystem muzzleFlash1;
    public ParticleSystem muzzleFlash2;
    float timer = 0f;
    bool saw = false; // variabile che dice se il giocatore è stato visto dal nemico

    // Start is called before the first frame update
    void Awake()
    {
        //animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //controllo se è ancora vivo l'object
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if (_navMeshAgent.enabled)
            {
                float dist = Vector3.Distance(Player.transform.position, transform.position);
                bool shoot = dist < AttackDistance;//false;
                bool follow = (dist < FollowDistance);

                if (follow)
                {
                    RaycastHit hit;
                    Vector3 fromPosition = bullet.transform.position;
                    Vector3 toPosition = Player.transform.position;
                    Vector3 direction = toPosition - fromPosition;
                    Physics.Raycast(bullet.transform.position, direction, out hit, 100f);
                    if (saw || hit.transform.tag == "Player") 
                    {
                        saw = true;
                        _navMeshAgent.SetDestination(Player.transform.position);
                    }
                }

                if (!follow || shoot)
                    _navMeshAgent.SetDestination(transform.position);

                if (follow)
                {
                    FaceTarget();
                }
                if (shoot && saw)
                {
                    FaceTarget();
                    timer += Time.deltaTime;
                    if (timer >= 1f)
                    {
                        timer = 0f;
                        Attack();
                        AudioManager.instance.Play("GunShoot");
                    }
                }
                if (!follow && !shoot)
                {

                }
            }
        }
    }

    public void Attack()
    {

        // metti un figlio e usa get component
        RaycastHit hit;
        //Ray MyRay = new Ray(bullet.transform.position, Vector3.forward);
        muzzleFlash1.Play();
        muzzleFlash2.Play();
        Vector3 fromPosition = bullet.transform.position;
        Vector3 toPosition = Player.transform.position;
        Vector3 direction = toPosition - fromPosition;

        //(Physics.Raycast(bullet.transform.position, bullet.transform.forward, out hit, 100f))
        if (Physics.Raycast(bullet.transform.position, direction, out hit, 100f))
        {
            if (Random.Range(1, 1000) < AttackProbability)
            {
                Debug.Log(hit.transform.name);
                Debug.DrawRay(bullet.transform.position, Vector3.forward * 100f, Color.green);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                    target.TakeDamage(damage);
            }
        }
    }
    IEnumerator Waiter(float time)
    {
        yield return new WaitForSeconds(time);
    }
    void FaceTarget()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));//d.x,0,x.z
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
