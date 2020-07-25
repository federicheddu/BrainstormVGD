using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarValueSetter : MonoBehaviour
{
    public Target target;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        target = gameObject.GetComponentInParent<Target>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
        {
            slider.value = (float)(target.health / target.maxHealt);
            if(target.health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
