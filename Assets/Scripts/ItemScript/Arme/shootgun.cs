
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class shootgun : MonoBehaviour,IFire
{
    Ray mouseRay;
    Vector3 direction;
    Viseur viseurScript;
    ArmesDistance item;
    float cooldown;
    float nextFire;
    
    public AudioClip son;
    public CharacterCombat scriptCombat;
    public GameObject BallePrefab;

    // Start is called before the first frame update
    void Start()
    {
        viseurScript = GameObject.Find("Player").GetComponent<Viseur>();
    }

    

    // Update is called once per frame
    /*
    void Update()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Vector3 target = mouseRay.GetPoint(maxRange);

        if(Physics.Raycast(mouseRay,out RaycastHit hit, maxRange))
        {
            target = hit.point;
        }

        Debug.DrawLine(viseur.transform.position, target,Color.green);
        direction = (target - viseur.transform.position).normalized;
    }
    */

    public void Fire()
    {
        if(Time.time >= nextFire)
        {
            
            Debug.Log("fire");
            Vector3 direction = viseurScript.direction;
            item = (ArmesDistance)scriptCombat.equip_Item;
            cooldown = item.cooldown;
            nextFire = Time.time + cooldown;
            StartCoroutine(sonRecharge());
            
            float maxRange = item.distance_balles;
            var vitesse = item.vitesse_balles;
            Transform[] Spawns = item.spawnBalle;
            Debug.Log(Spawns.Length);
            foreach (Transform spawn in Spawns)
            {
                StartCoroutine(lauchBalle(direction, maxRange, spawn.position, vitesse));
            }
        }
        
    }

    IEnumerator lauchBalle(Vector3 direction, float range, Vector3 spawn,float vitesseBalle)
    {
        var Balle = Instantiate(BallePrefab);
        Balle.transform.position = spawn;
        float distance_parcouru = 0;
        Vector3 pointInitial = spawn;
        while(distance_parcouru < range)
        {
            
            if(Balle != null)
            {
                Balle.transform.position +=  direction * vitesseBalle * Time.deltaTime;
                
                distance_parcouru = Vector3.Distance(pointInitial, Balle.transform.position);
                yield return null;
            }
            else
            {
                break;
            }
        }
        Destroy(Balle);
        if(Balle == null)
        {
            yield return null;
        }
        
    }

    IEnumerator sonRecharge()
    {
        yield return new WaitForSeconds(cooldown - 0.5f);
        GameManager.PlayAudioAndDie(item.itemInHand.transform, son, 0.5f);
        
    }
}
