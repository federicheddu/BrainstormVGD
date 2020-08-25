using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float speed;
    public float rotationSpeed1;
    public float rotationSpeed2;
    // Start is called before the first frame update
    void Start()
    {
        GameSettings.GetCheckpoint();

        Debug.Log("DIO");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 49)
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed1 * Time.deltaTime, Space.Self);
        transform.Translate(new Vector3(0, 0, 1) * rotationSpeed2 * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DIOC");
        Transform nemico = collision.transform;
        TargetLink targetLink = nemico.GetComponent<TargetLink>();
        if (targetLink != null)
        {
            Debug.Log("DIOCA");
            Target targetNemico = targetLink.target;
            targetNemico.TakeDamage(10000);
        }
    }

    private void FixedUpdate()
    {
    }
}
