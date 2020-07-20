using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    private PowerUp pu;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        pu = GetComponent<PowerUp>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        // Caso in cui si ha il powerup no damage
        // - i nemici NON devono avere il component powerup
        // - non sono nello stesso if per non fare nullpointerexception nel caso dei nemici
        if (pu != null)
            if (pu.nodamage)
                return;

        health -= damage;
        if (health <= 0f)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
