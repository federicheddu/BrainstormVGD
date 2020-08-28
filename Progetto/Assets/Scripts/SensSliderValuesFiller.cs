using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensSliderValuesFiller : MonoBehaviour
{
    private PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        UnityEngine.UI.Slider s = GetComponent<UnityEngine.UI.Slider>();
        s.minValue = GameSettings.MIN_SENS;
        s.maxValue = GameSettings.MAX_SENS;
        float v = GameSettings.GetMouseSensibility();
        Debug.Log(v.ToString());
        s.value = v;

        s.onValueChanged.AddListener(delegate { pm.setMouseSensibility(s.value); });

    }
}
