using UnityEngine;
using TMPro;

public class CharacterLife : MonoBehaviour
{
    public float playerHp;
    public float playerMaxHp;
    private GameObject barreHp;
    private RectTransform rectBarreHp;
    public TextMeshProUGUI textBarreHp;
    private CharacterDeplacement script_deplacementPlayer;
    public Animator animations;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        script_deplacementPlayer = GetComponent<CharacterDeplacement>();
        animations = script_deplacementPlayer.animations;
        if(textBarreHp != null)
        {
            textBarreHp.text = "";
        }
        barreHp = GameObject.Find("barreViePlayer");
        rectBarreHp = barreHp.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Regler la taille de la barre de vie
        playerHp = playerHp > playerMaxHp ? playerMaxHp : playerHp < 0 ? 0 : playerHp; 
        textBarreHp.text = $"Vie.s : {playerHp}";
        rectBarreHp.localScale = new Vector3(playerHp / playerMaxHp, 1.0f, 1.0f);
       
    }


    public void Resurrection()
    {
        isDead = false;
        playerHp = playerMaxHp;
        animations.SetBool("Resurrection", true);

    }
}
