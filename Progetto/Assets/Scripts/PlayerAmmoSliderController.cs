using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoSliderController : MonoBehaviour
{
    private PickWeapon pickWeaponScript;
    private GameObject currentWeapon;

    private Slider slider;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        pickWeaponScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickWeapon>();
        if(pickWeaponScript == null)
        {
            Destroy(gameObject);
        }

        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("1");
        currentWeapon = pickWeaponScript.getActiveWeaponGameObject();
        Debug.Log("2");

        if (currentWeapon != null)
        {
            Debug.Log("3");

            Gun gunScript = currentWeapon.GetComponent<Gun>();
            Debug.Log("4");

            int max = (int)gunScript.getMag();
            int current = max - ((int)gunScript.getFiredBullets());
            slider.maxValue = max;
            slider.value = current;
            text.text = current.ToString() + "/" + max.ToString();
        }
    }
}
