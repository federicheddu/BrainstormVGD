using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

public class PlayerMovement : MonoBehaviour
{

    //input
    public float vertical, horizontal, rotation = 0f;
    public bool run, crouch, jump;
    
    //movimento base
    private Vector3 direction = Vector3.zero;               //direzione movimento
    private float speed;
    public float walkSpeed, runSpeed, walkCap, runCap;      //fattori moviemento orizzontale
    public bool grounded, doubleJump;

    //salto e wallride
    private Vector3 jumpDirection, groundNormal;
    public float jumpPower, airCorrection;
    private double maxAirSpeed;
    public float wallJumpPower = 10f;
    public float wallRideGravity = -0.2f;

    //countermovement
    public float maxSpeed = 15;
    public float counterMovement = 0.175f;
    public float slideCounterMovement = 0.2f;
    private float threshold = 0.01f;

    //camera
    public Camera camera;
    private float tiltCamera = 0f;
    public float cameraVerticalMax = 90f;
    public float cameraVerticalMin = -90f;
    public float mouseSens = 5f;

    //altro
    private Vector3 fullDim;
    private Vector3 halfDim;

    //Debug
  /*public float groundNormalX;
    public float groundNormalY;
    public float groundNormalZ;*/


    //component
    CharacterController characterController;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;

        //dimensione
        fullDim = transform.localScale;
        halfDim = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        //A me il gioco funziona bene solo quando move sta in update quindi terrò questo commento qui finchè non capirò perchè
        Move();
    }

    private void FixedUpdate()
    {
        //Move();        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            groundNormal = collision.GetContact(0).normal;

            if (groundNormal == transform.TransformVector(Vector3.left) || groundNormal == transform.TransformVector(Vector3.right))
                rb.AddForce(Vector3.Scale(Physics.gravity, new Vector3(wallRideGravity, wallRideGravity, wallRideGravity)), ForceMode.Force); //diminuisce dell'80% la gravità
        }
            

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            maxAirSpeed = Math.Sqrt(Math.Pow(rb.velocity.x,2) + Math.Pow(rb.velocity.z,2));
        }
    }

    public void PlayerInput()
    {
        //movement
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        rotation = Input.GetAxis("Mouse X");
        jump = Input.GetButtonDown("Jump");
        run = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);

        //camera
        tiltCamera -= Input.GetAxis("Mouse Y");
        /** Camera X è gestito dalla rotazione del corpo **/
    }

    public void Move()
    {

        //velocità
        if (grounded)
            groundMove();
        else
            airMove();

        //diminuisce lo slittamento a terra una volta mollato wasd
        CounterMovement(direction.x, direction.z, new Vector2(rb.velocity.x, rb.velocity.z));

        //salto
        if (jump && grounded)
        {
            jumpDirection = groundNormal + Vector3.up;
            rb.AddForce(jumpDirection.normalized * jumpPower, ForceMode.Impulse);
        }

        //crouch
        if (crouch)
            transform.localScale = halfDim;
        else
            transform.localScale = fullDim;

        //rotazione corpo e camera
        transform.Rotate(0f, rotation * mouseSens, 0f);
        camera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(tiltCamera * mouseSens, cameraVerticalMin, cameraVerticalMax), 0f, 0f);
    }

    private void groundMove()
    {
        float force_x = 0, force_z = 0;
        speed = run ? runSpeed : walkSpeed;
        maxSpeed = run ? runCap : walkCap;

        if (Math.Sqrt(Math.Pow(rb.velocity.x, 2) + Math.Pow(rb.velocity.z, 2)) < speed)
        {
            force_x = horizontal * speed * Time.deltaTime;
            force_z = vertical * speed * Time.deltaTime;
        }

        direction = new Vector3(force_x, 0f, force_z);
        rb.AddForce(transform.TransformVector(direction), ForceMode.Impulse);
    }

    private void airMove()
    {
        float force_x = 0, force_z = 0;

        if (Math.Sqrt(Math.Pow(rb.velocity.x, 2) + Math.Pow(rb.velocity.z, 2)) < maxAirSpeed)
        {
            force_x = horizontal * airCorrection * Time.deltaTime;
            force_z = vertical * airCorrection * Time.deltaTime;
        }

        direction = new Vector3(force_x, 0f, force_z);
        rb.AddForce(transform.TransformVector(direction), ForceMode.Impulse);
    }

    //https://github.com/DaniDevy/FPS_Movement_Rigidbody/blob/master/PlayerMovement.cs
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jump) return;

        //Slow down sliding
        if (crouch)
        {
            rb.AddForce(speed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement
        if (Mathf.Abs(mag.x) > threshold && Mathf.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(speed * transform.TransformVector(Vector3.right) * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Mathf.Abs(mag.y) > threshold && Mathf.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(speed * transform.TransformVector(Vector3.forward) * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    public void setMouseSens(float sens)
    {
        mouseSens = sens;
    }

}
