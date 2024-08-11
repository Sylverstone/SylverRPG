using UnityEngine;
using TMPro;
using static UnityEditor.Progress;

public class PickItems : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Inventaire inventaire_player;
    public TextMeshProUGUI uitext;
    public Bonus items;
    public ArmesDistance items1;
    public ArmesPoing items2;
    void Start()
    {
        items = GetComponent<Bonus>();
        items1 = GetComponent<ArmesDistance>();
        items2 = GetComponent<ArmesPoing>();

        player = GameObject.FindGameObjectWithTag("Player");
        inventaire_player = player.GetComponent<Inventaire>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if(items1 != null)
            {
                inventaire_player.addObjectToInventory(items1);
            }
            else if (items != null)
            {
                inventaire_player.addObjectToInventory(items);
            }
            else if(items2 != null)
            {
                inventaire_player.addObjectToInventory(items2);
            }
            
            gameObject.SetActive(false);
            uitext.text = "";
        }
    }
}
