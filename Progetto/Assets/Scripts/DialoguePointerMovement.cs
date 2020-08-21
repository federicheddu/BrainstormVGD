using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePointerMovement : MonoBehaviour
{
    public bool goUp;
    private float delay = 1f;
    private float speed = 1f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        goUp = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (timer > delay) // ogni delay secondi cambia direzione
        {
            goUp = !goUp;
            timer = timer - delay;
        }

        // Movimento
        if (goUp)
        {
            transform.Translate(Vector3.left * -speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
    }
}
