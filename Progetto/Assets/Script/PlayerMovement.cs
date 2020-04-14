using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //input
    public float vertical, horizontal, rotation = 0f;
    public bool run, crouch, jump;
    

    //movimento base
    private Vector3 direction = Vector3.zero;
    private Vector3 jumpDirection;
    public float speed;
    public float walkSpeed = 4f, runSpeed = 6f, jumpPower = 5f, airSpeed;
    public float airDrag = 0.5f;
    public bool grounded, wasGrounded, wallRide;

    //movimento parkour
    private Vector3 groundNormal;
    public float wallJumpPower = 10f;
    public float wallRideGravity = -0.2f;

    //camera
    public Camera camera;
    private float tiltCamera = 0f;
    public float cameraVerticalMax = 90f;
    public float cameraVerticalMin = -90f;
    public float mouseSens = 5f;

    //altro
    private Vector3 fullDim;
    private Vector3 halfDim;

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
            speed = run ? runSpeed : walkSpeed;

        /*if (grounded && run && !wasRunning)
            direction.z = runSpeed;
        else if (grounded && run && wasRunning)
            direction.z = walkSpeed;*/

        //movimento a terra
        direction = new Vector3(horizontal * speed * Time.deltaTime, 0f, vertical * speed * Time.deltaTime);
        //direction = transform.TransformDirection(direction); Il problema era questa riga che non so perchè fosse qui
        transform.Translate(direction); //Movimento a terra




        //salto
        if (jump && grounded)
        {
            jumpDirection = groundNormal + Vector3.up;
            rb.AddForce(jumpDirection.normalized * jumpPower, ForceMode.Impulse);
            speed = speed / (airDrag + 1);
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


    private void OnCollisionEnter(Collision collision)
    {
        groundNormal = collision.GetContact(0).normal;
        
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "Wall")
            if(groundNormal == transform.TransformVector(Vector3.left) || groundNormal == transform.TransformVector(Vector3.right))
                rb.AddForce(Vector3.Scale(Physics.gravity, new Vector3(wallRideGravity, wallRideGravity, wallRideGravity)), ForceMode.Force); //diminuisce dell'80% la gravità

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
