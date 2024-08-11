using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporteur : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public GameObject autreTeleport;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collide");
            
            var position = autreTeleport.transform.position;
            Debug.Log(autreTeleport.transform.position);
            player.transform.position = new Vector3(position.x, player.transform.position.y, position.z);
            Debug.Log(autreTeleport.transform.position.ToString());
            player.transform.Translate(Vector3.forward * 5);
        }
    }

    
}
