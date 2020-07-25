﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public enum GunType
{
    Pistol,
    Assault,
    LMG,
    None
}

public class Gun : MonoBehaviour
{

    //Tipo di arma
    public GunType gunType;
    //pistola
    private float pisDamage = 10;
    private float pisMag = 8;
    private float pisHsMult = 2;
    //assalto
    private float assDamage = 10;
    private float assMag = 25;
    private float assHsMult = 1.8f;
    private float assFireRate = 8;
    //lmg
    private float lmgDamage = 8;
    private float lmgMag = 80;
    private float lmgHsMult = 1.5f;
    private float lmgFireRate = 15;

    //impostazioni sparo
    private float damage;
    private float headShot;
    private float headMult = 1f;
    private float damageMult = 1f;
    private float mag;
    private float bulletsFired = 0f;
    private float fireRate;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    private float range = 100f;

    //component utili
    public Camera camera;
    public PowerUp pu;

    //effetti
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        switch(gunType)
        {
            case GunType.Pistol:
                damage = pisDamage;
                mag = pisMag;
                headShot = pisHsMult;
                break;
            case GunType.Assault:
                damage = assDamage;
                mag = assMag;
                headShot = assHsMult;
                fireRate = assFireRate;
                break;
            case GunType.LMG:
                damage = lmgMag;
                mag = lmgMag;
                headShot = lmgMag;
                fireRate = lmgFireRate;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //check powerup
        if (isReloading)
            return;

        if (!pu.doubledamage) damageMult = 1;
        else damageMult = 2;

        //check bang
        if (Input.GetButton("Fire1") && bulletsFired < mag)
        {
            if(Time.time >= nextTimeToFire && gunType != GunType.Pistol) {      //armi automatiche (assalto e lmg)
                nextTimeToFire = Time.time + 1f / fireRate;
                bulletsFired++;
                Shoot();
            } else if(gunType == GunType.Pistol){                               //pistola
                bulletsFired++;
                Shoot();
            }
        } else if(Input.GetButtonDown("Fire1")) {                               //ricarica automatica
            StartCoroutine(Reload(2));
            return;
        }

        if (Input.GetKeyDown("r") && bulletsFired != 0)                         //ricarica manuale
        {
            StartCoroutine(Reload(2));
            return;
        }
    }

    IEnumerator Reload(float time)
    {
        isReloading = true;
        Debug.Log("Reloading");
        //animazione set bool true
        yield return new WaitForSeconds(time-0.25f);
        //animazion.setBool false
        yield return new WaitForSeconds(0.25f);
        bulletsFired = 0;
        isReloading = false;
    }

    //bang
    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Transform enemy = hit.transform;
            TargetLink targetlink = enemy.GetComponent<TargetLink>();

            if (targetlink != null) {
                Target target = targetlink.target;

                string enemy_tag = enemy.tag;
                if (enemy_tag == "Head") headMult = 2;
                else headMult = 1;

                target.TakeDamage(damage * headMult * damageMult);
            }

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * 30f);

            GameObject obj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(obj, 2f)
;

            if(enemy.tag == "Bullet")
            {
                Destroy(enemy.gameObject);
                return;
            }

            if (enemy.gameObject.GetComponent<TargetLink>())
            {
                TargetLink targetlink = enemy.GetComponent<TargetLink>();

                if (targetlink == null) return;
                Target target = targetlink.target;

                string enemy_tag = enemy.tag;
                if (enemy_tag == "Head") headMult = 2;
                else headMult = 1;

                target.TakeDamage(damage * headMult * damageMult);
            }
            else if (enemy.gameObject.GetComponent<LinkBoss>())
            {
                LinkBoss linkBoss = enemy.GetComponent<LinkBoss>();
                linkBoss.TakeDamage(damage * damageMult);
            }
        }
    }

}
