using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDeplacement : MonoBehaviour
{
    public Animator animations;
    //Script
    private CharacterLife script_charecterLife;
    //vitesse deplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    public float currentSpeed;

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
    public Vector3 jumpVector;
    BoxCollider playerCollider;
    private bool isGrounded;
    private bool jumping = false;
    public string[] listOfTrigger;
    private bool isDead;
    private bool CanRun;
    public bool jumpButtonPress = false;

    private void Awake()
    {
        playerControl = new PlayerControl();
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
        
        script_charecterLife = GetComponent<CharacterLife>();
        isDead = script_charecterLife.isDead;
        rb = GetComponent<Rigidbody>();
        listOfTrigger = new string[] { "Walk","Run","WalkBack","Jump" };
        animations = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<BoxCollider>();
    }

    
    // Update is called once per frame
    void Update()
    {
       
        if (!isDead)
        {
            CanRun = updateAnim();
            currentSpeed = CanRun ? currentSpeed : walkSpeed;
            transform.Translate(0, 0, moveDirection.y * currentSpeed * Time.deltaTime);
            transform.Rotate(0, moveDirection.x * turnSpeed * Time.deltaTime, 0);
        }
        

        /*
        if (Input.GetKey(avancer) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            transform.Translate(0, 0, walkSpeep * Time.deltaTime);
            if (!(jumping || Input.GetKeyDown(KeyCode.Space)))
            {
                resetBool();
                animations.SetBool("Walk",true);
                isAnim = true;
            }
        }
        else if (Input.GetKey(avancer) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            transform.Translate(0, 0, runSpeed * Time.deltaTime);
            if (!(jumping || Input.GetKeyDown(KeyCode.Space)))
            {
                resetBool();
                animations.SetBool("Run",true);
                isAnim = true;

            }
        }

        else if (Input.GetKey(reculer))
        {
            transform.Translate(0, 0, -(walkSpeep / 2) * Time.deltaTime);
            resetBool();
            animations.SetBool("WalkBack",true);
            isAnim = true;
        }
        if (Input.GetKey(droite))
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(gauche))
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !Input.GetKey(reculer))
        {
            resetBool();
            Debug.Log("space");
            animations.SetBool("Jump",true);
            Vector3 v = rb.velocity;
            v.y = jumpVector.y;
            rb.velocity = v;
            isAnim = true;
            StartCoroutine(waitJump());
            isGrounded = false;
        }

        if (!isAnim)
        {
            resetBool();
        }
        */

    }

    public bool updateAnim()
    {
        bool canrun = true;
        if(moveDirection == Vector2.zero && !jumping)
        {
            resetBool();
        }
        if (moveDirection.y != 0) 
        {
            Debug.Log(moveDirection);
            if (!jumping)
            {
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
                    animations.SetBool("WalkBack", true);
                    canrun = false;
                }
            }
        }
        else
        {
            if (!jumping) 
            { 
                resetBool(); 
            }
            
        }
        return canrun;
    }

    public void moving(InputAction.CallbackContext context)
    {
        var touche = context.control.displayName;
        Debug.Log(context.phase + " : " + touche);
        if (!isDead)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                moveDirection = context.action.ReadValue<Vector2>();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {         
                moveDirection = Vector2.zero;
            }
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
            if(context.phase == InputActionPhase.Performed && isGrounded)
            {
                resetBool();
                Debug.Log("jump");
                animations.SetBool("Jump", true);
                Vector3 v = rb.velocity;
                v.y = jumpVector.y;
                rb.velocity = v;
                jumping = true;
                isGrounded = false;
                StartCoroutine(waitJump());
            }
        }
    }

    public void resetBool()
    {
        foreach(string bool_ in listOfTrigger)
        {
            animations.SetBool(bool_, false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet entre en collision avec le sol
        if (collision.gameObject.CompareTag("Sol"))
        {   
            isGrounded = true;
            Debug.Log("touche le sol");
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
            Debug.Log("touche pas le sol");
        }
    }

    IEnumerator waitJump()
    {
        yield return new WaitForSeconds(0.2f);
        resetBool();
    }
}
