using System.Collections;
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
        if (pu.doubledamage) damageMult = 2;
        else damageMult = 1;

        //check reload
        if (isReloading) return;

        //distinzione comportamento in base all'arma
        switch(gunType)
        {
            /* PISTOLA */
            case GunType.Pistol:

                if (Input.GetMouseButtonDown(0) && bulletsFired < mag)
                {
                    bulletsFired++;
                    AudioManager.instance.Play("GunShoot");
                    Shoot();
                }
                else if (Input.GetMouseButtonDown(0))
                    StartCoroutine(Reload(2));

                break;

            /* ARMI AUTOMATICHE */
            case GunType.Assault:
            case GunType.LMG:

                if (Input.GetMouseButton(0) && bulletsFired < mag && Time.time < nextTimeToFire)
                {
                    bulletsFired++;
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
                else if (Input.GetMouseButton(0))
                    StartCoroutine(Reload(2f));

                break;
        }

        //ricarca automatica
        if (Input.GetKeyDown("r") && bulletsFired > 0)
            StartCoroutine(Reload(2));
    }

    IEnumerator ReloadSound()
    {
        AudioManager.instance.Play("GunReload");
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Reload(float time)
    {
        isReloading = true;
        Debug.Log("Reloading");
        StartCoroutine(ReloadSound());
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

            if (targetlink != null)
            {
                Target target = targetlink.target;

                string enemy_tag = enemy.tag;
                if (enemy_tag == "Head") headMult = 2;
                else headMult = 1;

                target.TakeDamage(damage * headMult * damageMult);
            }

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * 30f);

            GameObject obj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(obj, 2f);
        }
    }

}
