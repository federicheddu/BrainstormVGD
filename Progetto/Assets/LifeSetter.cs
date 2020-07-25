using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSetter : MonoBehaviour
{
    private Target target;

    private Slider slider;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
        slider = GetComponent<Slider>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slider != null)
        {
            slider.value = (float)(target.health / target.maxHealt);            
        }
        if(text != null)
        {
            text.text = target.health.ToString();
        }
    }

}
