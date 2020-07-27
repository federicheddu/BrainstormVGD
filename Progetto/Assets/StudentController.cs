using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    Coroutine c;
    Animator a;
    // Start is called before the first frame update
    void Start()
    {
        c = StartCoroutine(cor());
        a = GetComponent<Animator>();
        a.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator cor()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            switch(Mathf.Ceil(Random.Range(0, 3.9f)))
            {
                case 0:
                    a.SetBool("isAskingQuestion", true);
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", false);
                    break;
                case 1:
                    a.SetBool("isAskingQuestion", false);
                    a.SetBool("isTalking", true);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", false);
                    break;
                case 2:
                    a.SetBool("isAskingQuestion", false);
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", true);
                    a.SetBool("isIdle", false);
                    break;
                case 3:
                    a.SetBool("isAskingQuestion", false);
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", true);
                    break;


            }

        }
    }
}
