using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSetter : MonoBehaviour
{
    private Target target;

    public Slider slider;
    public Text text;

    public GameObject[] toActiveOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
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
        if(target.health <= 0)
        {
            foreach(GameObject gm in toActiveOnDeath)
            {
                gm.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

}
