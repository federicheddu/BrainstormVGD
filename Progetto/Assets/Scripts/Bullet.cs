using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 3.5f;
    float bulletLife = 10f;
    float timer = 0f;
    int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > bulletLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Target target = collision.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage);
            AudioManager.instance.Play("Bubble");
            Destroy(gameObject);
        }
    }
}
