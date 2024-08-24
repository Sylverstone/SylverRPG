using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
        listOfTrigger = new string[] { "Walk","Run","WalkBack","Jump","HeavyAttack","QuickAttack","Resurrection" };
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
        

    }

    public bool updateAnim()
    {
        bool canrun = true;
        if(moveDirection == Vector2.zero && !jumping && !scriptCombat.isAttacking)
        {
            resetBool();
        }
        if (moveDirection.y != 0) 
        {
            if (!jumping && !scriptCombat.isAttacking)
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
            if (!jumping && !scriptCombat.isAttacking) 
            { 
                resetBool(); 
            }
            
        }
        return canrun;
    }

    public void moving(InputAction.CallbackContext context)
    {
        var touche = context.control.displayName;
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

    IEnumerator waitJump()
    {
        yield return new WaitForSeconds(0.2f);
        resetBool();
    }
}
