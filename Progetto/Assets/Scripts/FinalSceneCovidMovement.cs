using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneCovidMovement : MonoBehaviour
{
    public float speed = 50;

    public float goUp = -1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        GameObject sphereOne = GameObject.FindGameObjectWithTag("Player");
        transform.Translate(Vector3.down * Time.deltaTime * (speed/20) * goUp);

        transform.RotateAround(sphereOne.transform.position, new Vector3(0, 1, 0), speed * Time.deltaTime);
    }

    IEnumerator ChangeDirection()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.5f);
            goUp *= -1;

        }
    }
}
