using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class CharacterDeplacement : MonoBehaviour
{
    public Animator animations;
    //Script
    private CharacterCombat scriptCombat;
    private CharacterLife script_charecterLife;
    //vitesse deplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    public float currentSpeed;
    public float groundDistance;

    //Inputs
    /*
    public string avancer;
    public string reculer;
    public string droite;
    public string gauche;
    */

    public Vector2 moveDirection;
    public PlayerControl playerControl;
    private InputAction move;
    private InputAction run;
    private InputAction jump;

    private Rigidbody rb;
    public float jumpForce;
    SphereCollider playerCollider;
    private bool isGrounded;
    private bool jumping = false;
    public string[] listOfBool;
    private bool isDead;
    private bool CanRun;
    private bool grounded = true;

    public Transform footLocation;

    private void Awake()
    {
        playerControl = GameManager.playerControl;
    }

    private void OnEnable()
    {
        playerControl.Player.Enable();

        move = playerControl.Player.Move;
        move.performed += moving;
        move.started += moving;
        move.canceled += moving;

        run = playerControl.Player.Run;
        run.performed += running;
        run.started += running;
        run.canceled += running;

        jump = playerControl.Player.Jump;
        jump.performed += jumpPress;
        jump.canceled += jumpPress;
        jump.started += jumpPress;
    }

    private void OnDisable()
    {
        
        move.performed -= moving;
        move.canceled -= moving;
        move.started -= moving;
        
        run.performed -= running;
        run.canceled -= running;
        run.started -= running;

        jump = playerControl.Player.Jump;
        jump.performed -= jumpPress;
        jump.canceled -= jumpPress;
        jump.started -= jumpPress;

        playerControl.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

        scriptCombat = GetComponent<CharacterCombat>();
        script_charecterLife = GetComponent<CharacterLife>();
        isDead = script_charecterLife.isDead;
        rb = GetComponent<Rigidbody>();
        animations = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        Debug.DrawRay(footLocation.position, new Vector3(0,-groundDistance,0), duration: 10, color: Color.red);
    }


    void FixedUpdate()
    {
       
        if (!isDead)
        {
            CanRun = updateAnim();
            currentSpeed = CanRun ? currentSpeed : walkSpeed;
            //transform.Translate(0, 0, moveDirection.y * currentSpeed * Time.deltaTime);
            rb.MovePosition(transform.position + transform.forward * moveDirection.y * currentSpeed*Time.deltaTime);
            //transform.Rotate(0, moveDirection.x * turnSpeed * Time.deltaTime, 0);
            rb.MoveRotation(transform.rotation *  Quaternion.Euler(0,moveDirection.x * turnSpeed * Time.deltaTime,0));
        }
        

    }

    public bool updateAnim()
    {
        bool canrun = true;
        if(moveDirection == Vector2.zero && grounded && !scriptCombat.isAttacking)
        {
            Debug.Log("reset 1");
            resetBool();
        }
        if (moveDirection.y != 0) 
        {
            if (grounded && !scriptCombat.isAttacking)
            {
                Debug.Log("reset 4");
                resetBool();
                if (moveDirection.y > 0) //avance
                {
                    if (currentSpeed == walkSpeed)
                    {
                        animations.SetBool("Walk", true);
                    }
                    else
                    {
                        animations.SetBool("Run", true);
                    }
                }
                else //recule
                {
                    animations.SetBool("BackWalk", true);
                    canrun = false;
                }
            }
        }
        else
        {
            if (grounded && !scriptCombat.isAttacking) 
            {
                Debug.Log("reset 2");
                resetBool(); 
            }
            
        }
        return canrun;
    }

    public void moving(InputAction.CallbackContext context)
    {
        if (!isDead)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void running(InputAction.CallbackContext context) 
    {
       
        if (context.phase == InputActionPhase.Canceled)
        {
            currentSpeed = walkSpeed;

        }else if(context.phase == InputActionPhase.Performed)
        {
            currentSpeed = runSpeed;
        }
    }

    public void jumpPress(InputAction.CallbackContext context)
    {
        if (!isDead)
        {
            Debug.Log(IsGrounded());
            if (context.phase == InputActionPhase.Performed && grounded)
            {
                resetBool();
                animations.SetBool("Jump", true);
                rb.velocity = transform.up * jumpForce;
                jumping = true;
                grounded = false;
                StartCoroutine(waitUntilTouchGround());
            }
        }
    }

    public void resetBool()
    {
        
        foreach(string bool_ in listOfBool)
        {
            animations.SetBool(bool_, false);
        }
        
    }
    

    IEnumerator waitUntilTouchGround()
    {
        Debug.Log("true");
        yield return new WaitWhile(IsGrounded);
        Debug.Log("is grounded false");
        yield return new WaitUntil(IsGrounded);
        Debug.Log("is grounded true");
        Debug.Log("reset 3");
        grounded = true;
        resetBool();
    }

    public bool IsGrounded()
    {
        
        return Physics.Raycast(footLocation.position, -transform.up, groundDistance);
    }



    /*
     * 
     * 
    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet entre en collision avec le sol
        if (collision.gameObject.CompareTag("Sol"))
        {   
            isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            isGrounded = true;
            if (jumping)
            {
                isGrounded = true;
                jumping = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Vérifier si l'objet quitte la collision avec le sol
        if (collision.gameObject.CompareTag("Sol"))
        {
            isGrounded = false;
        }
    }
    */


}
