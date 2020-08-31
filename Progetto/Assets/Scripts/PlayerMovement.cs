using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

public class PlayerMovement : MonoBehaviour
{

    //input
    public float vertical, horizontal, rotation = 0f;
    public bool key_run, key_crouch, key_jump;

    //movimento base
    public float antiInclinazione;
    private Vector3 direction = Vector3.zero;               //direzione movimento
    private float speed;
    public float walkSpeed, runSpeed, walkCap, runCap;      //fattori moviemento orizzontale
    public bool grounded;
    public bool doubleJump = false;
    public bool doubleJumpPrevFrame = false;

    //salto e wallride
    private Vector3 groundNormal;
    public float jumpPower, airCorrection;
    public double maxAirSpeed;
    public float wallJumpPower = 10f;
    public float wallRideGravity = -0.2f;
    public bool walled;
    public float antiGravita;

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
    private float mouseSens = 2f; // Attributo aggiunto piuttosto che fare ogni volta GameSettings.getMouseSensibility() perchè più leggero, anzichè leggerlo ogni volta chiamando il metodo viene salvato e letto normalmente
    public float zRotation;
    public float cameraRotationSpeed = 100;

    //altro
    private Vector3 fullDim;
    private Vector3 halfDim;

    //Debug
    /*public float groundNormalX;
      public float groundNormalY;
      public float groundNormalZ;*/

    float timer = 0f;

    //component
    CharacterController characterController;
    private Rigidbody rb;
    private PowerUp pu;

    //gameobject figli
    public GameObject gunPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pu = GetComponent<PowerUp>();
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
        {
            grounded = true;
            if (pu.doublejump) //riattivo anche il double jump in caso di powerup
                doubleJump = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        groundNormal = collision.GetContact(0).normal;
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;

            if ((groundNormal == transform.TransformVector(Vector3.left) || groundNormal == transform.TransformVector(Vector3.right)) && Math.Sqrt(Math.Pow(rb.velocity.x, 2) + Math.Pow(rb.velocity.z, 2)) > 0)
            {
                rb.AddForce(Physics.gravity * Time.deltaTime, ForceMode.Impulse);
                rb.AddForce(Vector3.Cross(groundNormal, Vector3.up) * speed *  Time.deltaTime, ForceMode.Impulse);
            }

            //rb.AddForce(Vector3.Scale(Physics.gravity, new Vector3(wallRideGravity, wallRideGravity, wallRideGravity)), ForceMode.Force); //diminuisce dell'80% la gravità
        }


        if (collision.gameObject.tag == "Wall")
        {
            walled = true;

            if (Math.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z) > 0) {
                rb.AddForce(Physics.gravity * antiGravita * -1);
                rb.AddForce(groundNormal * -1);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            rb.useGravity = true;
            //maxAirSpeed = Math.Sqrt(Math.Pow(rb.velocity.x, 2) + Math.Pow(rb.velocity.z, 2)); Ho commentato questa riga perchè mi impediva di muovermi in aria M.S.
        }
        if (collision.gameObject.tag == "Wall")
        {
            walled = false;
        }
    }

    public void PlayerInput()
    {
        //movement
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        rotation = Input.GetAxis("Mouse X");
        key_jump = Input.GetButtonDown("Jump");
        key_run = Input.GetKey(KeyCode.LeftShift);
        key_crouch = Input.GetKey(KeyCode.LeftControl);

        //camera
        tiltCamera -= Input.GetAxis("Mouse Y");
        /** Camera X è gestito dalla rotazione del corpo **/
    }

    public void Move()
    {
        //velocità
        if (grounded)
        {
            rb.drag = 1;
            groundMove();
        }
        else
        {
            rb.drag = 0;
            airMove();
        }

        //diminuisce lo slittamento a terra una volta mollato wasd
        CounterMovement(direction.x, direction.z, new Vector2(rb.velocity.x, rb.velocity.z));

        //salto
        if (doubleJumpPrevFrame != pu.doublejump && grounded)
            doubleJump = pu.doublejump;
        doubleJumpPrevFrame = pu.doublejump;

        if (key_jump)
            jump();

        //crouch
        if (key_crouch)
        {
            transform.localScale = halfDim;
            gunPosition.transform.localScale = new Vector3(1, 2, 1);
        }
        else
        {
            transform.localScale = fullDim;
            gunPosition.transform.localScale = new Vector3(1, 1, 1);
        }

        //rotazione corpo e camera
        transform.Rotate(0f, rotation * mouseSens, 0f);
        camera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(tiltCamera * mouseSens, cameraVerticalMin, cameraVerticalMax), 0f, 0f);
    }

    private void groundMove()
    {
        float force_x = 0, force_z = 0;
        speed = key_run ? runSpeed : walkSpeed;
        maxSpeed = key_run ? runCap : walkCap;

        if (Math.Sqrt(Math.Pow(rb.velocity.x, 2) + Math.Pow(rb.velocity.z, 2)) < speed)
        {
            force_x = horizontal * speed * Time.deltaTime;
            force_z = vertical * speed * Time.deltaTime;
        }

        //so che x e z sono invertiti, ma mi servono invertiti per fare il prodotto vettoriale
        //uso questo approccio per avere la tangente al terreno con il giusto angolo e permettere di fare le salite facilmente
        /*
        direction = new Vector3(force_z, 0f, -force_x);
        direction = Vector3.Cross(direction, groundNormal);
        rb.AddForce(transform.TransformVector(direction), ForceMode.Impulse);*/// ho commentato questa parte perchè il personaggio rimaneva incastrato sui muri M.S

        if (groundNormal.y > 0.5)
            rb.AddForce(transform.TransformVector(new Vector3(force_x, (1 - groundNormal.y) * antiInclinazione * 0.5f, force_z)), ForceMode.Impulse);
        else
            rb.AddForce(transform.TransformVector(new Vector3(force_x, 0, force_z)), ForceMode.Impulse);
        //Ho risolto temporaneamente il problema dell'inclinazione in questa maniera

        float s = Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)));

        timer += Time.deltaTime;
        if (s < 1f)
        {
            timer = 0;
        }
        else
        if (timer >= 0.5f && s <= 8f && s > 1f)
        {
            StartCoroutine(WalkSound());
            timer = 0f;
        }
        else if (s > 8f && timer > 0.3f)
        {
            timer = 0f;
            StartCoroutine(WalkSound());
        }
    }

    IEnumerator WalkSound()
    {
        AudioManager.instance.Play("Walk");
        yield return new WaitForSeconds(0.5f);
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

    private void jump()
    {
        Vector3 jumpDirection = Vector3.zero;

        if ((grounded || walled) && groundNormal.y < 0.19)
            jumpDirection = groundNormal + Vector3.up;
        else if(grounded)
            jumpDirection = new Vector3(0,1,0) + Vector3.up;

        if (!grounded && doubleJump)
        {
            jumpDirection = Vector3.up;
            doubleJump = false;
        }

        rb.AddForce(jumpDirection.normalized * jumpPower, ForceMode.Impulse);
    }

    //https://github.com/DaniDevy/FPS_Movement_Rigidbody/blob/master/PlayerMovement.cs
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || key_jump) return;

        //Slow down sliding
        if (key_crouch)
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

    public void setMouseSensibility(float sens)
    {
        mouseSens = sens;
    }


}
