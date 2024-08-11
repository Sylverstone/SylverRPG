using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseDescItem : MonoBehaviour
{
    public bool isOpen = false;
    public VoirItems voirItems;
    public PlayerControl playerControl;
    InputAction cancel;

    private void OnEnable()
    {
        playerControl = new PlayerControl();
        playerControl.UI.Enable();
        cancel = playerControl.UI.Cancel;
        cancel.performed += onCancel;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void onCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            voirItems.Close();
        }
    }
}
