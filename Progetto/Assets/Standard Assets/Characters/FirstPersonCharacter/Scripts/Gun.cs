using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //impostazioni sparo
    public float damage = 10f;
    private int headMult = 1;
    private int damageMult = 1;
    private float range = 100f;
    int maxGun = 8;
    int countGun;
    bool isReloading = false;
    //component utili
    public Camera camera;
    public PowerUp pu;

    //effetti
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetButtonDown("Fire1") && countGun < maxGun)
        {
            countGun++;
            Shoot();
        }else if(Input.GetButtonDown("Fire1") && countGun >= maxGun)
        {
            StartCoroutine(Reload(2));
            return;
        }
        if (Input.GetKeyDown("r") && countGun != 0)
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
        countGun = 0;
        isReloading = false;
    }

    //bang
    void Shoot() {

        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)) {

            Debug.Log(hit.transform.name);

            Transform enemy = hit.transform;
            TargetLink targetlink = enemy.GetComponent<TargetLink>();

            if (targetlink == null) return;
            Target target = targetlink.target;

            string enemy_tag = enemy.tag;
            if (enemy_tag == "Head") headMult = 2;
            else headMult = 1;

            target.TakeDamage(damage * headMult * damageMult);

        }
    }
}
