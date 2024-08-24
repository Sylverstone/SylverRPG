using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cible : MonoBehaviour,IDestructible
{
    public AudioClip son;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestructSelf()
    {
        Destroy(gameObject);
    }
}
