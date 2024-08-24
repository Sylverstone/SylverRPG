
using UnityEngine;
using TMPro;

public class rotateText : MonoBehaviour
{
    
    Transform player;

    public TextMeshPro Text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Text != null)
        {
            Vector3 player_rotationAngle = player.transform.eulerAngles;
            //permet à l'objet d'être tourné comme le joueur
            Text.transform.eulerAngles = new Vector3(Text.transform.eulerAngles.x, player_rotationAngle.y, Text.transform.eulerAngles.z);
        }
    }


}
