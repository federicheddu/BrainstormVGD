using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StudentController : MonoBehaviour
{
    public GameObject pointer;
    Animator a;
    Coroutine c;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        a.SetBool("isIdle", true);
        c = StartCoroutine(ChangeState());
    }

    public void Talk()
    {
        a.SetBool("isTalking", true);
        a.SetBool("isClapping", false);
        a.SetBool("isIdle", false);
        pointer.SetActive(true);
        StopCoroutine(c);
    }

    public void Idle()
    {
        a.SetBool("isTalking", false);
        a.SetBool("isClapping", false);
        a.SetBool("isIdle", true);
        pointer.SetActive(false);
        StopCoroutine(c);
    }
    public void AskQuestion()
    {
        a.SetBool("isTalking", false);
        a.SetBool("isClapping", false);
        a.SetBool("isIdle", false);
        pointer.SetActive(false);
        StopCoroutine(c);
    }
    public void Clap()
    {
        a.SetBool("isTalking", false);
        a.SetBool("isClapping", true);
        a.SetBool("isIdle", false);
        pointer.SetActive(false);
        StopCoroutine(c);
    }


    IEnumerator ChangeState()
    {
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    a.SetBool("isTalking", true);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", false);
                    break;
                case 1:
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", true);
                    a.SetBool("isIdle", false);
                    break;
                case 2:
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", true);
                    break;
            }
            yield return new WaitForSeconds(Random.Range(0f, 5f));
        }
    }
}
