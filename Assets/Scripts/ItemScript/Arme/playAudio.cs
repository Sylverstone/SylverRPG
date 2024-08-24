using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{

    public AudioClip song;
    [SerializeField] float spaceSong = 1;
    // Start is called before the first frame update
    void Start()
    {
       GameManager.PlayAudioAndDie(transform, song, spaceSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
