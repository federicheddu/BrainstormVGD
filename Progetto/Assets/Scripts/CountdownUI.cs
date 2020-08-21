﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    public Text countdownNumber;

    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        if(countdownNumber != null)
        {
            int i = 3;
            while (i > 0)
            {
                countdownNumber.text = i.ToString();
                yield return new WaitForSeconds(1f);
                i--;
            }
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
        
        finished = true;
    }

    public bool hasFinished() { return finished; }
}
