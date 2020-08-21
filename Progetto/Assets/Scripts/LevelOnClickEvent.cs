using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOnClickEvent : MonoBehaviour
{
    public GameObject[] toDisable;
    public GameObject[] toEnable;
    public GameObject[] toTurnWhite;
    public Image[] opacityToDecrease;

    public void Click()
    {
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].SetActive(false);
        }
        for (int i = 0; i < toEnable.Length; i++)
        {
            toEnable[i].SetActive(true);
        }
        for(int i=0; i < toTurnWhite.Length; i++)
        {
            Image img = toTurnWhite[i].GetComponent<Image>();
            if(img != null)
            {
                img.color = Color.white;
            }
            Text text = toTurnWhite[i].GetComponent<Text>();
            if(text != null)
            {
                text.color = Color.white;
            }
        }
        for(int i=0; i<opacityToDecrease.Length; i++)
        {
            Color c = opacityToDecrease[i].color;
            c.a = 0.3f;
            opacityToDecrease[i].color = c; 
        }
    }
    
}
