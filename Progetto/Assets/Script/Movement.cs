using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //input
    public float x, y;
    public bool jump, crouch, sprint, grapling;

    //camera
    public Camera camera;
    private float tiltCamera = 0;

    //component
    private CharacterController characterController;



    //movimento
    private Vector3 direction = Vector3.zero;

    //moltiplicatori movimento
    private float walkSpeed = 5f;
    private float runSpeed = 1.5f;
    private float jumpForce = 0.1f;
    private float gravity = 25f;
    private float sens = 5.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        //eliminare il cursore all'avvio
        Cursor.lockState = CursorLockMode.Locked;

        //recupero i component
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        sprint = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);

        //movimento su x e y con WASD
        direction = new Vector3(x * walkSpeed * Time.deltaTime, direction.y, y * walkSpeed * Time.deltaTime);
        direction = transform.TransformDirection(direction);

        //chissà cosa fa
        if (characterController.isGrounded && jump)
            direction.y = jumpForce;

        if (characterController.isGrounded && sprint)
            direction.z *= runSpeed;

        //rotazione corpo/camera
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * sens, 0.0f);
        tiltCamera -= Input.GetAxis("Mouse Y") * sens;
        camera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(tiltCamera, -90f, 90f), 0f, 0f);

        //gravità
        direction.y -= gravity * Time.deltaTime * Time.deltaTime;

        //movimento
        characterController.Move(direction);
    }
}