public class SeeNameArmesPoing : SeeName
{
    ArmesPoing items;
    // Start is called before the first frame update
    void Start()
    {
       init();
       items = GetComponent<ArmesPoing>(); 
    }

    private void OnMouseOver()
    {
        if(calculDistance(gameObject.transform.position, player.transform.position))
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
