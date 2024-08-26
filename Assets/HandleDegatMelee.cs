using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDegatMelee : MonoBehaviour
{

    public PartieMelee partie;

    CharacterCombat scriptCombat;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scriptCombat = GetComponentInParent<CharacterCombat>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != player)
        {            
            scriptCombat.HandleMeleeFight(partie,collision.gameObject);
        }
        
    }
}
