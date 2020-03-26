using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15f;
    public float gravity = -19.81f;
    public float jump = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    bool isGrounded;
    Vector3 velocity;
    
    // Update is called once per frame
    void Update()
    {
        //crea una sfera con posizione di groundCheck e raggio groundDistance per verificare 
        //se il giocatore sta toccando terra groundMask serve a specificare quale layer viene 
        // considerato ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // crea vettore di movimento sommando le componenti laterali e "frontali"
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime ;
        controller.Move(velocity* Time.deltaTime);
    }
}