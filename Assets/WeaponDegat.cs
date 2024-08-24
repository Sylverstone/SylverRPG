
using System.Linq.Expressions;
using UnityEngine;

public class WeaponDegat : MonoBehaviour
{
    GameObject player;
    CharacterCombat characterCombat;
    public typesItem typeItem;
    // Start is called before the first frame update
    private void OnEnable()
    {
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterCombat = player.GetComponent<CharacterCombat>();   
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name);
        if(collision.gameObject != player)
        {
            if (characterCombat.isAttacking || typeItem == typesItem.ArmesDistance)
            {
                if (gameObject.activeSelf)
                {
                    IDamageable i = collision.gameObject.GetComponent<IDamageable>();
                    if (i != null)
                    {
                        if(typeItem == typesItem.ArmesPoing)
                        {
                            ArmesPoing item = (ArmesPoing)characterCombat.equip_Item;
                            bool HeavyAttack = characterCombat.MakeHeavyAttack;
                            if (HeavyAttack)
                            {
                                i.takeDamage(item.degatLourd);
                            }
                            else
                            {
                                i.takeDamage(item.degatNormal);
                            }
                            
                        }
                        else
                        {
                            
                            ArmesDistance item = (ArmesDistance)characterCombat.equip_Item;
                            i.takeDamage(item.DegatParBalle);
                            Destroy(gameObject);
                        }                        
                        
                    }
                    
                    var IDestructible = collision.gameObject.GetComponent<IDestructible>();
                    if (IDestructible != null)
                    {
                        IDestructible.DestructSelf();
                        if(typeItem == typesItem.ArmesDistance)
                        {
                            characterCombat.isAttacking = false;
                            Destroy(gameObject);
                        }
                    }                    
                    
                }
            }
        }                
        
    }

}
