using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossesLivesSetter : MonoBehaviour
{
    public Target[] target;

    private Slider slider;
    private Text text;
    private float bossesTotalLife;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
        bossesTotalLife = 0f;
        foreach(Target t in target)
        {
            bossesTotalLife += t.maxHealt;
        }
        slider.maxValue = bossesTotalLife;
    }

    // Update is called once per frame
    void Update()
    {
        float life = 0f;
        foreach(Target t in target)
        {
            if(t != null)
            {
                life += t.health;
            }
        }
        if(life > 0)
        {
            slider.value = life;
            text.text = (slider.value.ToString() + "   ").Substring(0, 3);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
