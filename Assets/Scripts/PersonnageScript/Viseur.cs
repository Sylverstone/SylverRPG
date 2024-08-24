
using UnityEngine;

public class Viseur : MonoBehaviour
{
    CharacterCombat scriptCombat;
    ArmesDistance ArmeEquip;
    Ray mouseRay;

    public Vector3 direction;
    [SerializeField] float range;
    [SerializeField] float vitesseBalle;
    
    // Start is called before the first frame update
    void Start()
    {
        scriptCombat = GetComponent<CharacterCombat>();        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(scriptCombat.type_item == typesItem.ArmesDistance)
        {
            ArmeEquip = (ArmesDistance)scriptCombat.equip_Item;
            Vector3 viseur = ArmeEquip.viseurLocation.position;
            range = ArmeEquip.distance_balles;
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 target = mouseRay.GetPoint(range);

            if (Physics.Raycast(mouseRay, out RaycastHit hit, range))
            {
                target = hit.point;
            }
            Debug.DrawLine(viseur, target);
            direction = (target - viseur).normalized;
        }
    }
}
