
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoirItems : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contenaireDescription;
    public TextMeshProUGUI contenaire_nom_item;
    public TextMeshProUGUI contenaire_desc_item;
    public TextMeshProUGUI contenaire_attribut_item;
    public TextMeshProUGUI contenaire_type_item;
    public Image contenaire_icone_item;
    public CloseDescItem closeDescItem;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void AfficheItems(string nom_items, string desc_items,
        string attribut_items,string type_item, Sprite icon_items)
    {
        contenaireDescription.SetActive(true);
        contenaire_nom_item.text = nom_items;
        contenaire_desc_item.text = desc_items;
        contenaire_attribut_item.text = attribut_items;
        contenaire_icone_item.sprite = icon_items;
        contenaire_type_item.text = type_item;
        closeDescItem.isOpen = true;
    }

    public void Close()
    {
        contenaireDescription.SetActive(false);
        contenaire_nom_item.text = "";
        contenaire_desc_item.text = "";
        contenaire_attribut_item.text = "";
        contenaire_icone_item.sprite = null;
        contenaire_type_item.text = "";
        closeDescItem.isOpen = false;
    }
}
