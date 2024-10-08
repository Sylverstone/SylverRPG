using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterCombat : MonoBehaviour,IDamageable
{
    couleur Color;
    CharacterLife script_vie;
    CharacterDeplacement script_deplacement;
    Animator animator;
    InputAction HeavyAttack;
    InputAction NormalAttack;
    InputAction Fire;

    InputAction Jab;
    InputAction LegToHead;
    InputAction CoupAuCorp;
    InputAction Uppercut;

    PlayerControl playerControl;

    public float degatMelee;
    public float multiplicateurFist = 1f;
    public float multiplicateurLeg = 1.2f;
    public float degatArme = 1;
    public Armes equip_Item = null;
    public typesItem type_item;
    public bool MakeHeavyAttack;
    public bool isAttacking;
    public bool haveMelee = false;
    public float tempPasserSansMelee;
    public Dictionary<string, float> dicoTempAnim = new Dictionary<string, float>
    {
        {"Heavy",1f},
        {"Quick",1.09f }
    };
    // Start is called before the first frame update
    void Start()
    {
        script_deplacement = GetComponent<CharacterDeplacement>();
        script_vie = GetComponent<CharacterLife>();
        Color = new couleur();
        playerControl = GameManager.playerControl;
        animator = GetComponent<CharacterDeplacement>().animations;
        StartCoroutine(getTimeWithoutMelee());
    }

    private void OnEnable()
    {
        playerControl.Melee.Enable();
        Jab = playerControl.Melee.Jab;
        LegToHead = playerControl.Melee.LegToHead;
        CoupAuCorp = playerControl.Melee.CoupCorp;
        Uppercut = playerControl.Melee.Uppercut;



    }
    public void ActiveCombat()
        
    {
        playerControl.Combat.Enable();
        HeavyAttack = playerControl.Combat.HeavyAttack;
        NormalAttack = playerControl.Combat.QuickAttack;
        Fire = playerControl.Combat.Fire;

        if(type_item == typesItem.ArmesPoing)
        {
            HeavyAttack.performed += onAttack;
            NormalAttack.performed += onAttack;
        }
        else
        {
            Fire.performed += onAttack;
        }
        
        
        
    }

    public void  DisactiveCombat()
    {
        playerControl.Combat.Disable();
        HeavyAttack.performed -= onAttack;
        Fire.performed -= onAttack;
        NormalAttack.performed -= onAttack ;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void equipItemInHand()
    {
        equip_Item.itemInHand.SetActive(true);
        Debug.Log($"{equip_Item.name} is active");
        ActiveCombat();
    }

    public void removeItemInHand()
    {
        equip_Item.itemInHand.SetActive(false);
        Debug.Log($"{equip_Item.name} was removed");
        DisactiveCombat();

    }

    public void takeDamage(float damage)
    {
        script_vie.playerHp -= damage;
        if (script_vie.playerHp <= 0)
        {
            script_vie.isDead = true;
            animator.Play("Death");
            ChangeCam script = GetComponent<ChangeCam>();
            script.chooseCam();
        }
        else
        {

        }

    }

    void onAttack(InputAction.CallbackContext context)
    {
        
        Debug.Log("on attack");
        if(type_item == typesItem.ArmesPoing)
        {
            isAttacking = true;
            if (context.action.name == "HeavyAttack")
            {
                Debug.Log("in");
                script_deplacement.resetBool();
                StartCoroutine(resetIsAttacking("Heavy"));
                script_deplacement.animations.SetBool("HeavyAttack", true);
            }
            else if (context.action.name == "QuickAttack")
            {
                Debug.Log("in");
                script_deplacement.resetBool();
                StartCoroutine(resetIsAttacking("Quick"));
                animator.SetBool("QuickAttack", true);
            }
        }
        else
        {
            Debug.Log("fire");
            var IFire = equip_Item.itemInHand.GetComponent<IFire>();
            if(IFire != null)
            {
                IFire.Fire();
            }
        }

    }

    void onMelee(InputAction.CallbackContext context)
    {
        script_deplacement.resetBool();
        animator.SetBool("Combat", true);
        tempPasserSansMelee = 0;
        if (context.action.name == "Jab")
        {
            animator.SetBool("Jab", true);
        }
        else if(context.action.name == "LegToHead")
        {
            animator.SetBool("LegToHead", true);
        }
        else if(context.action.name == "Uppercut")
        {
            animator.SetBool("Uppercut", true);
        }
        else if( context.action.name == "CoupCorp")
        {
            animator.SetBool("Coup au Corp", true);
        }
    }

    IEnumerator resetIsAttacking(string attaque)
    {
        float delta = dicoTempAnim[attaque];
        yield return new WaitForSeconds(delta);
        isAttacking = false;
    }

    public void KillEnnemieText(string name, TextMeshProUGUI text)
    {
        text.text = $"Vous avez battue <color={Color.couleurEnnemie}>" +
                     $"{name}";
        StartCoroutine(GameManager.effaceTextIn(text,3));
    }

    public void HandleMeleeFight(PartieMelee partie, GameObject cible)
    {
        Debug.Log(partie + " a touch�");
        IDamageable i = cible.GetComponent<IDamageable>();
        if(i != null)
        {
            if(partie == PartieMelee.Pied)
            {
                i.takeDamage(degatMelee * multiplicateurLeg);
            }
            if(partie == PartieMelee.Poing)
            {
                i.takeDamage(degatMelee * multiplicateurFist);
            }
        }
    }
    /*
    IEnumerator getTimeWithoutMelee()
    {

        while (true)
        {
            
            tempPasserSansMelee += Time.deltaTime;
            if(tempPasserSansMelee >= 10)
            {
                animator.SetBool("Combat", false);
                tempPasserSansMelee = 0;
            }
            
        }

    }
    */
}
