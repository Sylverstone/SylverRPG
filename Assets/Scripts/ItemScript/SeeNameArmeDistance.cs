
using Unity.VisualScripting;

public class SeeNameArmeDistance : SeeName
{
    ArmesDistance items;

    void Start()
    {
        init();
        items = GetComponent<ArmesDistance>();
    }

    private void OnMouseOver()
    {
        if (calculDistance(gameObject.transform.position, player.transform.position))
        {
            afficheNomItem(items, gameObject);
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            afficheNomItem(items, gameObject);
        }
    }
}
