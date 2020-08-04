using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Audio;

public class actions2 : MonoBehaviour
{

    public GameObject Player;
    public GameObject bullet;
    //private Animator animator;
    private NavMeshAgent _navMeshAgent;

    public float AttackDistance = 10.0f;
    public float FollowDistance = 22.0f;
    private float AttackProbability = 250f;
    public float damage = 10f;
    public AudioClip GunSound = null;
    public ParticleSystem muzzleFlash1;
    public ParticleSystem muzzleFlash2;
    float timer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        //animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
                    _navMeshAgent.SetDestination(Player.transform.position);
                }

                if (!follow || shoot)
                    _navMeshAgent.SetDestination(transform.position);

                if (follow)
                {
                    FaceTarget();
                }
                if (shoot)
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
        if (Physics.Raycast(bullet.transform.position, bullet.transform.forward, out hit, 100f))//Vector3.forward, out hit, 100f))
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

        /*
        if (m_Audio != null)
        {
            m_Audio.PlayOneShot(GunSound);
        }
        */
    }
    IEnumerator Waiter(float time)
    {
        yield return new WaitForSeconds(time);
    }
    void FaceTarget()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        Quaternion lookRotation =
            Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation =
            Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
