using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    public Camera camera;

    public float runSpeed = 5f;
    public float jumpSpeed = 0.5f;
    public float gravity = 20.0f;
    public float sens = 1000.0f;

    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //eliminare il cursore all'avvio
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.isGrounded)
        {
            //movimento su x e y con WASD
            direction = new Vector3(Input.GetAxis("Horizontal") * runSpeed, 0.0f, Input.GetAxis("Vertical") * runSpeed);
            direction = transform.TransformDirection(direction);

            //chissà cosa fa
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpSpeed;
            }

        }

        //rotazione del corpo con il mouse
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * sens, 0.0f);

        //tilt della camera
        camera.transform.Rotate((Input.GetAxis("Mouse Y") * sens) % 90f, 0.0f, 0.0f);
        
        //gravità
        direction.y -= gravity * Time.deltaTime * Time.deltaTime;

        //movimento
        characterController.Move(direction);
    }
}