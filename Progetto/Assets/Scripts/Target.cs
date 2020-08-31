using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float maxHealt = 50f;
    public float health = 50f;
    private PowerUp pu;
    private Animator animator;
    public float regTime = 5f;
    private float timerHit;
    private float count;
    GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        pu = GetComponent<PowerUp>();
        animator = GetComponent<Animator>();
        manager = GameObject.Find("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
        //controllo se è stato colpito meno di 5 secondi fa allora parte la rigenerazione della salute
        if(gameObject.tag == "Player")
        {
            if (Time.time - timerHit > regTime && health < maxHealt)
            {
               count += Time.deltaTime;
                if(count >= 1f)
                {
                    health += 2f;
                    Debug.Log(health);
                    count = 0f;
                }
            }
        }
    }


    public void TakeDamage(float damage)
    {
        if (gameObject.tag == "Player")
            timerHit = Time.time;

        // Caso in cui si ha il powerup no damage
        // - i nemici NON devono avere il component powerup
        // - non sono nello stesso if per non fare nullpointerexception nel caso dei nemici
        if (pu != null)
            if (pu.nodamage && damage != 100000)
                return;

        //il boss non prende danno se esiste ancora la corona tra i suoi figli
        if (gameObject.name == "Coronavirus2_hipoly" && (gameObject.transform.Find("Crown") || gameObject.transform.Find("Crown Variant").gameObject.activeInHierarchy)) //GetChild(1).name == "Crown")
            return;

        //controllo per vedere se il proprietario di target muore
        health -= damage;
        if (health <= 0f)
            Die();
    }

    void Die()
    {
        //caso siano nemici umanoidi
        if (animator != null)
        {
            animator.SetTrigger("Death");
            gameObject.GetComponent<Actions>().SetDeath();
            AudioManager.instance.Play("DeathHuman");
            gameObject.GetComponent<DropPu>().Drop();
            Destroy(gameObject, 2);
        }
        else if (gameObject.tag == "BossShooter")
            //caso siano lo shooter
        {
            Target target = transform.parent.gameObject.GetComponent<Target>();
            target.TakeDamage(20);
            //caso bullet
        }else if (gameObject.tag == "Bullet")
        {
            AudioManager.instance.Play("Bubble");
            Destroy(gameObject);
        }
        else
        { 
            //caso clone corona
            if(gameObject.name == "Crown Variant")
            {
                gameObject.SetActive(false);
            }else 
            // caso sia robot attack
            if (gameObject.GetComponent<actions2>())
            {
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                AudioManager.instance.Play("Explosion");
                gameObject.GetComponent<DropPu>().Drop();
                Destroy(gameObject, 1);
                //caso BOSS
            }else if (gameObject.GetComponent<Rotate>())
            {
                Destroy(transform.parent.gameObject);
                //manager.GetComponent<AudioManager>().Victory();
            }
            else if(gameObject.tag != "Player")
                Destroy(gameObject);
        }
    }
}
