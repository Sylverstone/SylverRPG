using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Items> Inventaire_joueur = new List<Items>();
    public GameObject inventaire;
    public CharacterLife script_vie;
    public CharacterDeplacement script_deplacement;
    public CharacterCombat script_combat;
    private int taille_inventaire = 0;
    private int max_inventaire = 3;

    void Start()
    {
        script_vie = GetComponent<CharacterLife>();
        script_deplacement = GetComponent<CharacterDeplacement>();
        script_combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void addObjectToInventory(Bonus objet)
    {
        if(taille_inventaire < max_inventaire)
        {
            Inventaire_joueur.Add(objet);
            Image[] images = inventaire.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                if(image.sprite == null)
                {
                    image.sprite = objet.spriteItem;
                    var script_item = image.gameObject.GetComponent<equipItem>();
                    script_item.typeItem = "Bonus";
                    script_item.itemInInventaire = (Items)objet;
                    break;
                }
            }

            int i = 0;
            foreach(int bonus in objet.bonus)
            {
                if (objet.bonus_pattern[i] == "Vie")
                {
                    Debug.Log("h");
                    var maxHp = script_vie.playerMaxHp;
                    var hp = script_vie.playerHp;
                    script_vie.playerMaxHp = maxHp + maxHp * bonus / 100;
                    script_vie.playerHp *= hp + hp * bonus / 100;                 
                }

                if(objet.bonus_pattern[i] == "Vitesse")
                {
                    var walkspeed = script_deplacement.walkSpeed;
                    var runspeed = script_deplacement.runSpeed;
                    var currentspeed = script_deplacement.currentSpeed;

                    script_deplacement.walkSpeed = walkspeed + walkspeed * bonus / 100;
                    script_deplacement.runSpeed = runspeed + runspeed * bonus/100;
                    script_deplacement.currentSpeed = currentspeed + currentspeed * bonus/100;
                }

                if (objet.bonus_pattern[i] == "Melee")
                {
                    var degatMelee = script_combat.degatMelee;
                    script_combat.degatMelee = degatMelee + degatMelee * bonus / 100; 
                }

                if (objet.bonus_pattern[i] == "Arme")
                {
                    script_combat.degatArme += bonus;
                }

                i++;
            }
        }
    }

    public void addObjectToInventory(ArmesPoing objet)
    {
        if (taille_inventaire < max_inventaire)
        {
            Inventaire_joueur.Add(objet);
            Image[] images = inventaire.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                if (image.sprite == null)
                {
                    image.sprite = objet.spriteItem;
                    var script_item = image.gameObject.GetComponent<equipItem>();
                    script_item.typeItem = "ArmesPoing";
                    script_item.itemInInventaire = (Items)objet;
                    break; 
                }
            }
        }
    }

    public void addObjectToInventory(ArmesDistance objet)
    {
        if (taille_inventaire < max_inventaire)
        {
            Inventaire_joueur.Add(objet);
            Image[] images = inventaire.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                if (image.sprite == null)
                {
                    image.sprite = objet.spriteItem;
                    var script_item = image.gameObject.GetComponent<equipItem>();
                    script_item.typeItem = "ArmesDistance";
                    script_item.itemInInventaire = (Items)objet;
                    break;
                }
            }
        }
    }


    public void clearInventory()
    {
        Inventaire_joueur.Clear();
    }
}
