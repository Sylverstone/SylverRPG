using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Enemy : MonoBehaviour,IDamageable
{
    public AudioClip DeadSong;
    public float EnemyLife;
    [SerializeField] float FullLife;
    public TextMeshPro Text_vie;
    public TextMeshProUGUI killText;
    public CharacterCombat scriptCombat;
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        if(Text_vie != null)
        {
            Text_vie.text = "";
        }
        FullLife = EnemyLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyLife < FullLife)
        {
            Text_vie.text = $"Vie : {EnemyLife}";   
        }
    }

    public void takeDamage(float damage)
    {
        EnemyLife -= damage;
        if(EnemyLife <= 0 && !isDead)
        {
            scriptCombat.KillEnnemieText(gameObject.name, killText);
            isDead = true;
            GameManager.PlayAudioAndDie(transform, DeadSong,TransformIsParent : false,spaceSong : 0.8f);
            Destroy(gameObject);
        }
    }
}
