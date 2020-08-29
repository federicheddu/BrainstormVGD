﻿using System.Collections;
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
      int a = GameSettings.GetCheckpoint();
        if (a == 0)
            transform.position = new Vector3(0, 0, 0);
        else if (a == 1)
            transform.position = new Vector3(0, 15, 0);
        else if (a == 2)
            transform.position = new Vector3(0, 25, 0);
        else
            transform.position = new Vector3(0, 40, 0);

        Debug.Log(a + "madonna impanata");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 64)
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed1 * Time.deltaTime, Space.Self);
        transform.Translate(new Vector3(0, 0, 1) * rotationSpeed2 * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        Transform nemico = other.transform;
        TargetLink targetLink = nemico.GetComponent<TargetLink>();
        if (targetLink != null)
        {
            Target targetNemico = targetLink.target;
            targetNemico.TakeDamage(100000);
        }
    }

    private void FixedUpdate()
    {
    }
}
