using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SeeName : MonoBehaviour
{
    public GameObject objectImage;
    public Image uiImage;
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
        if (objectImage != null)
        {
            uiImage = objectImage.GetComponent<Image>();
            objectImage.SetActive(false);
        }
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
            objectImage.SetActive(false);
            textNomItem.text = "";
        }        
    }


    public void afficheNomItem(Items items,GameObject objet)
    {
        if (objet.activeSelf && textNomItem != null)
        {
            objectImage.SetActive(true);
            uiImage.sprite = items.spriteItem;
            textNomItem.text = items.nom_item;
        }
    }

    /*
    public void afficheNomItem(ArmesPoing items, GameObject objet)
    {
        if (objet.activeSelf)
        {
            objectImage.SetActive(true);
            uiImage.sprite = items.spriteItem;
            textNomItem.text = items.nom_item;
        }
    }

    public void afficheNomItem(Bonus items, GameObject objet)
    {
        if (objet.activeSelf)
        {
            objectImage.SetActive(true);
            uiImage.sprite = items.spriteItem;
            textNomItem.text = items.nom_item;
        }
    }
    */

    public bool calculDistance(Vector3 point1, Vector3 point2)
    {
        Vector3 vecteur_distance = point1 - point2;
        return vecteur_distance.magnitude <= 10;
    }
    
}
