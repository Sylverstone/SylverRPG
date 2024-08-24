using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SeeName : MonoBehaviour
{
    public TextMeshPro textNomItem;
    public GameObject player;


    private void OnMouseExit()
    {
        clear();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            clear();
        }
    }

    void Update()
    {
        rotateNomText();
    }

    public void init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textNomItem = gameObject.GetComponentInChildren<TextMeshPro>();
        
        if (textNomItem != null)
        {
            textNomItem.text = "";
        }
    }

    public void rotateNomText()
    {
        //rotation du joueur
        if (textNomItem != null)
        {
            Vector3 player_rotationAngle = player.transform.eulerAngles;
            //permet à l'objet d'être tourné comme le joueur
            textNomItem.transform.eulerAngles = new Vector3(textNomItem.transform.eulerAngles.x, player_rotationAngle.y, textNomItem.transform.eulerAngles.z);
        }
    }

    public void clear()
    {
        if (textNomItem != null)
        {
            textNomItem.text = "";
        }        
    }


    public void afficheNomItem(Items items,GameObject objet)
    {
        if (objet.activeSelf && textNomItem != null)
        {
            textNomItem.text = items.nom_item;
        }
    }


    
}
