
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class equipItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Items itemInInventaire = null;
    public GameObject Player;
    public typesItem typeItem;
    public VoirItems voirItem;
    public GameObject indicateurPreSelect;
    public GameObject[] allIndicateurSelection;
    public GameObject indicateurSelection;

    CharacterCombat script_combat;

    // Start is called before the first frame update
    void Start()
    {
        voirItem = GetComponentInParent<VoirItems>();
        Player = GameObject.FindGameObjectWithTag("Player");
        script_combat  = Player.GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.playerControl.Combat.Disable();
        if (itemInInventaire != null)
        {
            indicateurPreSelect.SetActive(true);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.playerControl.Combat.Enable();
        indicateurPreSelect.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        // Vérifie quel bouton de la souris a été utilisé pour le clic
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemInInventaire != null && script_combat.equip_Item != itemInInventaire && itemInInventaire is Armes)
            {
                removeAllIndicateurSelect();
                indicateurSelection.SetActive(true);
                Debug.Log("equip");
                if(script_combat.equip_Item != null)
                {
                    //il a déjà un item equippé
                    script_combat.equip_Item.itemInHand.SetActive(false);
                }
                equipeItem((Armes)itemInInventaire, typeItem); 
            }
            else if (itemInInventaire != null && script_combat.equip_Item == itemInInventaire)
            {
                Debug.Log("desequip");
                desequipItem();
            }
            
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //sup item
            Debug.Log("Clic droit sur l'élément UI !");
            if (itemInInventaire != null)
            {
                voirItem.AfficheItems(itemInInventaire.nom_item, itemInInventaire.description, "attend après", typeItem.ToString(), itemInInventaire.spriteItem);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Clic central sur l'élément UI !");
            if (itemInInventaire != null)
            {
                voirItem.AfficheItems(itemInInventaire.nom_item, itemInInventaire.description, "attend après", typeItem.ToString(), itemInInventaire.spriteItem);
            }
        }    
    }

    public void equipeItem(Armes items, typesItem type_item)
    {
        if(type_item == typesItem.ArmesDistance || type_item == typesItem.ArmesPoing)
        {
            script_combat.equip_Item = items;
            script_combat.type_item = type_item;
            script_combat.equipItemInHand();
        }
        
    }

    public void desequipItem()
    {
        script_combat.equip_Item = null;
        script_combat.type_item = typesItem.None;
        script_combat.removeItemInHand();
    }

    public void removeAllIndicateurSelect()
    {
        foreach(GameObject select in allIndicateurSelection)
        {
            select.SetActive(false);
        }
    }
}
