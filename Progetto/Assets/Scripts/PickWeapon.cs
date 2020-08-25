using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{
    public Transform camera;
    public float range = 10f;
    public bool canPick = false;

    public GameObject pistol, assault, lmg;

    // Start is called before the first frame update
    void Start()
    {
        switch (GunType.Pistol)
        {
            case GunType.Pistol:
                pistol.SetActive(true);
                assault.SetActive(false);
                lmg.SetActive(false);
                break;
            case GunType.Assault:
                assault.SetActive(true);
                pistol.SetActive(false);
                lmg.SetActive(false);
                break;
            case GunType.LMG:
                lmg.SetActive(true);
                pistol.SetActive(false);
                assault.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Gun gun = hit.transform.GetComponent<Gun>();
            if (gun == null) return;


            canPick = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                switch(hit.transform.GetComponent<Gun>().gunType)
                {
                    case GunType.Pistol:
                        pistol.SetActive(true);
                        assault.SetActive(false);
                        lmg.SetActive(false); 
                        break;
                    case GunType.Assault:
                        assault.SetActive(true);
                        pistol.SetActive(false);
                        lmg.SetActive(false);
                        break;
                    case GunType.LMG:
                        lmg.SetActive(true);
                        pistol.SetActive(false);
                        assault.SetActive(false);
                        break;
                }

                Destroy(hit.transform.gameObject);
            }
        }
        else
            canPick = false;
    }

    public GunType getCurrentWeapon()
    {
        if (pistol.activeInHierarchy)
            return GunType.Pistol;
        else if (assault.activeInHierarchy)
            return GunType.Assault;
        else if (lmg.activeInHierarchy)
            return GunType.LMG;
        else
            return GunType.None;
    }

}
