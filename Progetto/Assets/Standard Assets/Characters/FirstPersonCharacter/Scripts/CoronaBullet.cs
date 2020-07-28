using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaBullet : MonoBehaviour
{
    GameObject player;
    public GameObject bullet;
    public float timer = 0;
    float AttackDistance = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        //ricerca del giocatore
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist < AttackDistance) {
            //controllo per vedere se lo shooter e entro 90 gradi di distanza dal giocatore
            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            if (angle <= 90f)
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    //creazione dei proiettili
                    GameObject bulletObject = Instantiate (bullet);
                    bulletObject.transform.position = transform.position;
                    bulletObject.transform.forward = transform.forward;

                    timer = 0f;
                }
            }
        }
    }
}
