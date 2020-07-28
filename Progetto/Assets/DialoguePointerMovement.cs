using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePointerMovement : MonoBehaviour
{
    private bool goUp;
    private float delay = 1f;
    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        goUp = true;
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            transform.Translate(Vector3.left * -speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            goUp = !goUp;
        }
    }
}
