using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //input
    public float vertical, horizontal, rotation = 0f;
    public bool run, crouch, jump;
    private bool wasRunning = false;
    public bool grounded, wasGrounded;

    //movimento
    private Vector3 direction = Vector3.zero;
    private float speed;
    public float walkSpeed = 4f, runSpeed = 6f, jumpForce = 0.1f;
    private float airTime = 0f;
    public float gravity = 25f;

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
    }

    private void FixedUpdate()
    {
        Move();
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
        /* Camera X è gestito dalla rotazione del corpo*/
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
        direction = transform.TransformDirection(direction);
        rb.AddForce(direction);


        //salto
        if (Input.GetButtonDown("Jump") && grounded)        
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);


        //crouch
        if (crouch)
            transform.localScale = halfDim;
        else
            transform.localScale = fullDim;

        //rotazione corpo e camera
        transform.Rotate(0f, rotation * mouseSens, 0f);
        camera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(tiltCamera, cameraVerticalMin, cameraVerticalMax), 0f, 0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            grounded = false;
        }
    }
}
