
using UnityEngine;
using UnityEngine.EventSystems;

public class equipItem : MonoBehaviour, IPointerClickHandler
{
    public Items itemInInventaire = null;
    public GameObject Player;
    public string typeItem;
    public VoirItems voirItem;
    

    // Start is called before the first frame update
    void Start()
    {
        voirItem = GetComponentInParent<VoirItems>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Vérifie quel bouton de la souris a été utilisé pour le clic
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            var player_arme = Player.GetComponent<CharacterCombat>();
            if (itemInInventaire != null && player_arme.equip_Item != itemInInventaire )
            {
                Debug.Log("equip");
                equipeItem(itemInInventaire, typeItem);
 
            }
            else if (itemInInventaire != null && player_arme.equip_Item == itemInInventaire)
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
                voirItem.AfficheItems(itemInInventaire.nom_item, itemInInventaire.description, "attend après", typeItem, itemInInventaire.spriteItem);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Clic central sur l'élément UI !");
            if (itemInInventaire != null)
            {
                voirItem.AfficheItems(itemInInventaire.nom_item, itemInInventaire.description, "attend après", typeItem, itemInInventaire.spriteItem);
            }
        }    
    }

    public void equipeItem(Items items, string type_item)
    {
        Player.GetComponent<CharacterCombat>().equip_Item = items;
        Player.GetComponent<CharacterCombat>().type_item = typeItem;
    }

    public void desequipItem()
    {
        Player.GetComponent<CharacterCombat>().equip_Item = null;
        Player.GetComponent<CharacterCombat>().type_item = "";
    }
}
