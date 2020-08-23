using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensSliderValuesFiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.UI.Slider s = GetComponent<UnityEngine.UI.Slider>();
        s.minValue = GameSettings.MIN_SENS;
        s.maxValue = GameSettings.MAX_SENS;
        float v = GameSettings.GetMouseSensibility();
        Debug.Log(v.ToString());
        s.value = v;
    }
}
