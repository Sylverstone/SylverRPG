
using UnityEngine;
using UnityEngine.InputSystem;

public class InspectorSyst : MonoBehaviour
{
    PlayerControl playerControl;
    InputAction mousePos;
    InputAction navigate;
    InputAction leftInspect;
    InputAction click;
    bool active;
    bool mouveWithArrow;


    Vector3 previousMousePos;
    Vector3 mousePosition;
    Vector2 direction;

    public GameObject InspectedObjet;
    public GameObject fond_flou;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 deltaPos;
    [SerializeField] bool moveInspect;

    // Start is called before the first frame update

    private void Awake()
    {
        playerControl = GameManager.playerControl;
    }

    public void ActiveInspection()
    {
        active = true;
        playerControl.Player.Disable();
        playerControl.Combat.Disable();
        mousePos = playerControl.UI.mousePos;
        mousePos.Enable();
        mousePos.performed += onMouseMove;

        navigate = playerControl.UI.Navigate;
        navigate.Enable();
        navigate.performed += onNavigate;
        navigate.canceled += onNavigate;

        leftInspect = playerControl.UI.LeftInspect;
        leftInspect.Enable();
        leftInspect.performed += DisactiveInspection;

        click = playerControl.UI.Click;
        click.Enable();
        click.performed += onClick;
        click.canceled += onClick;

    }

    public void DisactiveInspection(InputAction.CallbackContext ctx)
    {
        Debug.Log("desactive");
        active = false;
        playerControl.Player.Enable();
        playerControl.Combat.Enable();
        
        click.performed -= onClick;
        click.canceled -= onClick;
        click.Disable();
        leftInspect.performed -= DisactiveInspection;
        leftInspect.Disable();
        navigate.performed -= onNavigate;
        navigate.canceled -= onNavigate;
        navigate.Disable();
        mousePos.performed -= onMouseMove;
        mousePos.Disable();
        Destroy(InspectedObjet.transform.GetChild(0).gameObject);
        fond_flou.SetActive(false);
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (moveInspect)
            {
                deltaPos = mousePosition - previousMousePos;
                float rotaX = deltaPos.y * rotationSpeed/4 * Time.deltaTime;
                float rotaY = -deltaPos.x * rotationSpeed/4 * Time.deltaTime;

                Quaternion rotation = Quaternion.Euler(rotaX, rotaY, 0);
                InspectedObjet.transform.rotation = rotation * InspectedObjet.transform.rotation;
                previousMousePos = mousePos.ReadValue<Vector2>();
            }
            else
            {
                float rotaX = direction.y * rotationSpeed * Time.deltaTime;
                float rotaY = direction.x * rotationSpeed * Time.deltaTime;

                Quaternion rotation = Quaternion.Euler(rotaX, rotaY, 0);
                InspectedObjet.transform.rotation = rotation * InspectedObjet.transform.rotation;
            }

        }
    }

    void onMouseMove(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
    }

    void onNavigate(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    void onClick(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
        {
            moveInspect = true;
            previousMousePos = mousePos.ReadValue<Vector2>();
        }
        else
        {
            Debug.Log("release");
            moveInspect = false;
        }
        
    }
}
