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
        switch (GameSettings.GetWeapon())
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
        canPick = false;
        if (Physics.Raycast(camera.position, camera.forward, out hit, range))
        {
            string tag = hit.transform.tag;
            if (tag == "pistol" || tag == "assault" || tag == "lmg")
                canPick = true;
            else
                return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                switch(hit.transform.tag)
                {
                    case "pistol":
                        pistol.SetActive(true);
                        assault.SetActive(false);
                        lmg.SetActive(false);
                        GameSettings.SetWeapon(GunType.Pistol);
                        break;
                    case "assault":
                        assault.SetActive(true);
                        pistol.SetActive(false);
                        lmg.SetActive(false);
                        GameSettings.SetWeapon(GunType.Assault);
                        break;
                    case "lmg":
                        lmg.SetActive(true);
                        pistol.SetActive(false);
                        assault.SetActive(false);
                        GameSettings.SetWeapon(GunType.LMG);
                        break;
                }

                Destroy(hit.transform.gameObject);
            }
        }
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

    public GameObject getActiveWeaponGameObject()
    {
        if (pistol.activeInHierarchy)
            return pistol;
        else if (assault.activeInHierarchy)
            return assault;
        else if (lmg.activeInHierarchy)
            return lmg;
        else
            return null;
    }

}
