using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Items> Inventaire_joueur = new List<Items>();
    public GameObject inventaire;
    private CharacterLife script_vie;
    private CharacterDeplacement script_deplacement;
    private CharacterCombat script_combat;
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
                    script_item.typeItem = typesItem.Bonus;
                    script_item.itemInInventaire = (Items)objet;
                    break;
                }
            }
            ApplyBonus(objet.bonus, objet.bonus_pattern);

            
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
                    script_item.typeItem = typesItem.ArmesPoing;
                    script_item.itemInInventaire = (Items)objet;
                    break; 
                }
            }
            ApplyBonus(objet.buff.bonus, objet.buff.bonus_pattern);
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
                    script_item.typeItem = typesItem.ArmesDistance;
                    script_item.itemInInventaire = (Items)objet;
                    break;
                }
            }
            ApplyBonus(objet.buff.bonus, objet.buff.bonus_pattern);
        }
    }


    
    public void clearInventory()
    {
        Inventaire_joueur.Clear();
    }

    public void ApplyBonus(float[] bonusAdd, typesBonus[] bonus_pattern )
    {
        int i = 0;
        foreach (int bonus in bonusAdd)
        {
            if (bonus_pattern[i] == typesBonus.Vie)
            {
                Debug.Log("h");
                var maxHp = script_vie.playerMaxHp;
                var hp = script_vie.playerHp;
                script_vie.playerMaxHp = maxHp + maxHp * bonus / 100;
                script_vie.playerHp = hp + hp * bonus / 100;
            }

            if (bonus_pattern[i] == typesBonus.Vitesse)
            {
                var walkspeed = script_deplacement.walkSpeed;
                var runspeed = script_deplacement.runSpeed;
                var currentspeed = script_deplacement.currentSpeed;

                script_deplacement.walkSpeed = walkspeed + walkspeed * bonus / 100;
                script_deplacement.runSpeed = runspeed + runspeed * bonus / 100;
                script_deplacement.currentSpeed = currentspeed + currentspeed * bonus / 100;
            }

            if (bonus_pattern[i] == typesBonus.DegatMelee)
            {
                var degatMelee = script_combat.degatMelee;
                script_combat.degatMelee = degatMelee + degatMelee * bonus / 100;
            }

            if (bonus_pattern[i] == typesBonus.DegatArme)
            {
                var degatArme = script_combat.degatArme;
                script_combat.degatArme = degatArme + degatArme * bonus/100;
            }

            i++;
        }
    }
}
