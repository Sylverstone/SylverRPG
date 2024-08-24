using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PickItems : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Inventaire inventaire_player;
    public typesItem typeItem;
    public Items item;

    InputAction click;
    PlayerControl playerControl;
    bool takeItem = false;

    private void Awake()
    {
        playerControl = GameManager.playerControl;
    }
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        inventaire_player = player.GetComponent<Inventaire>();
    }

    private void OnEnable()
    {
        click = playerControl.Player.Choose;
        click.Enable();
        click.performed += OnChoose;
        click.canceled += OnChoose;
    }

    private void OnMouseOver()
    {
        playerControl.Combat.Disable();
        if (takeItem && GameManager.calculDistance(transform.position,player.transform.position)) 
        {
            if(typeItem == typesItem.ArmesPoing)
            {
                inventaire_player.addObjectToInventory((ArmesPoing)item);
            }
            else if(typeItem == typesItem.Bonus)
            {
                inventaire_player.addObjectToInventory((Bonus)item);    
            }
            else if (typeItem == typesItem.ArmesDistance)
            {
                inventaire_player.addObjectToInventory((ArmesDistance)item);
            }            
            gameObject.SetActive(false);
            playerControl.Combat.Enable();
        }
    }

    private void OnMouseExit()
    {
        playerControl.Combat.Enable();
    }

    public void OnChoose(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
        {
            takeItem = true;
        }
        else
        {
            takeItem = false;
        }
    }
}
