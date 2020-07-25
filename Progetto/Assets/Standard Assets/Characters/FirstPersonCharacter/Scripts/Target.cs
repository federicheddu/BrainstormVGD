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

    // Start is called before the first frame update
    void Start()
    {
        pu = GetComponent<PowerUp>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (pu.nodamage)
                return;

        //il boss non prende danno se esiste ancora la corona tra i suoi figli
        if (gameObject.name == "Coronavirus2_hipoly" && gameObject.transform.GetChild(1).name == "Crown")
            return;

        health -= damage;
        if (health <= 0f)
            Die();
    }

    void Die()
    {
        //caso siano nemici umanoidi
        if (animator != null)
                {
                    Debug.Log("ha l'animator");
                    animator.SetTrigger("Death");
                    Destroy(gameObject, 2);
                }
        else if (gameObject.tag == "BossShooter")
            //caso siano lo shooter
        {
            Target target = transform.parent.gameObject.GetComponent<Target>();
            target.TakeDamage(20);
        }else 
        { 
            if (gameObject.GetComponent<MeshFilter>() == null)
        {
            Debug.Log("OK");
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject, 1);
        }
                 else
                    Destroy(gameObject);
        }
    }
}
