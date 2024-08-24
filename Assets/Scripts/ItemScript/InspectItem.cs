using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InspectItem : MonoBehaviour
{
    PlayerControl playerControl;
    InputAction inspect;
    bool inspectItem;
    bool collide;
    couleur Color;


    public Vector3 inspectOrientation;
    public TextMeshProUGUI consigneText;
    public InspectorSyst inspectorSyst;
    // Start is called before the first frame update
    private void Awake()
    {
        playerControl = GameManager.playerControl;
    }

    private void OnEnable()
    {
        inspect = playerControl.Player.Inspect;
        inspect.Enable();
        inspect.performed += onInspect;
        inspect.canceled += onInspect;

    }
    void Start()
    {
        Color = new couleur();
        if (consigneText != null)
        {
            consigneText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inspectItem && collide)
        {
            GameObject itemCopy = Instantiate(gameObject);
            itemCopy.transform.SetParent(inspectorSyst.InspectedObjet.transform);
            itemCopy.transform.localPosition = Vector3.zero;
            Component[] components = itemCopy.GetComponentsInChildren<Component>();
            foreach (Component component in components)
            {
                if(component is not Transform && component is not MeshRenderer && 
                    component is not MeshFilter)
                {
                    Destroy(component);
                }
            }
            
            itemCopy.transform.localRotation = Quaternion.Euler(inspectOrientation);
            inspectorSyst.InspectedObjet.transform.localRotation = Quaternion.Euler(0, 0, 0);
            inspectorSyst.fond_flou.SetActive(true);
            inspectorSyst.ActiveInspection();
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision : " + gameObject.name);
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collide = true;
            consigneText.text = $"<color={Color.couleurIndication}>Appuyez</color> sur " +
                            $"<color={Color.couleurIndication}>E</color> pour Inspectez";
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collide = false;
            consigneText.text = "";
        }
    }

    public void onInspect(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
        {
            inspectItem = true;
        }
        else
        {
            inspectItem = false;
        }
    }
}
