using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActualState { idle, talking, asking, clapping};

public class StudentController : MonoBehaviour
{
    public GameObject pointer;
    Animator a;
    ActualState state;
    Coroutine c;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        a.SetBool("isIdle", true);
        state = ActualState.idle;
        c = StartCoroutine(ChangeState());
    }

    public void Talk()
    {
        a.SetBool("isAskingQuestion", false);
        a.SetBool("isTalking", true);
        a.SetBool("isClapping", false);
        a.SetBool("isIdle", false);
        state = ActualState.talking;
        pointer.SetActive(true);
        StopCoroutine(c);
    }

    public void Idle()
    {
        a.SetBool("isAskingQuestion", false);
        a.SetBool("isTalking", false);
        a.SetBool("isClapping", false);
        a.SetBool("isIdle", true);
        state = ActualState.idle;
        pointer.SetActive(false);
        StopCoroutine(c);
    }
    public void AskQuestion()
    {
        a.SetBool("isAskingQuestion", true);
        a.SetBool("isTalking", false);
        a.SetBool("isClapping", false);
        a.SetBool("isIdle", false);
        state = ActualState.asking;
        pointer.SetActive(false);
        StopCoroutine(c);
    }
    public void Clap()
    {
        a.SetBool("isAskingQuestion", false);
        a.SetBool("isTalking", false);
        a.SetBool("isClapping", true);
        a.SetBool("isIdle", false);
        state = ActualState.clapping;
        pointer.SetActive(false);
        StopCoroutine(c);
    }

    public ActualState getState() { return state; }

    IEnumerator ChangeState()
    {
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    a.SetBool("isAskingQuestion", false);
                    a.SetBool("isTalking", true);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", false);
                    break;
                case 1:
                    a.SetBool("isAskingQuestion", false);
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", true);
                    a.SetBool("isIdle", false);
                    break;
                case 2:
                    a.SetBool("isAskingQuestion", false);
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", true);
                    break;
                case 3:
                    a.SetBool("isAskingQuestion", true);
                    a.SetBool("isTalking", false);
                    a.SetBool("isClapping", false);
                    a.SetBool("isIdle", false);
                    break;
            }
            yield return new WaitForSeconds(Random.Range(0f, 5f));
        }
    }
}
