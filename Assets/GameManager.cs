using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq.Expressions;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static string[] etatJeu = { "Pause", "Actif" };
    public static bool GamePaused = false;
    public static MenuPause menuPause;
    public static PlayerControl playerControl;
    public static Vector3 spawn;
    public GameObject player;
    public GameObject sphere;
    public Texture2D cursor;
    public Vector2 sourisPoint = new Vector2(0.5f, 0.5f);

    InputAction quit;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Plus de une instance du gm");
            return;
        }
        Instance = this;
        spawn = GameObject.Find("SPAWN").transform.position;
        GameManager.BackToSpawn(player, spawn);
        playerControl = new PlayerControl();
        
        menuPause = GameObject.Find("MenuPause").GetComponent<MenuPause>();
    }

    private void OnEnable()
    {
        quit = playerControl.UI.Quit;
        quit.Enable();
        quit.performed += onQuit;

    }

    private void OnDisable()
    {
        quit.Disable();
        quit.performed -= onQuit;
    }

    private void Start()
    {
        //PauseGame();
    }

    public static void endGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void Update()
    {

        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
        */
    }

    public void onQuit(InputAction.CallbackContext ctx)
    {
        togglePause();
    }

    void togglePause()
    {
        switch (GamePaused)
        {
            case false:
                PauseGame(); break;
            case true:
                unPauseGame(); break;
        }
    }

    public static void PauseGame()
    {
        GamePaused = true;
        menuPause.Active();
        Time.timeScale = 0f;
    }

    public static void unPauseGame()
    {
        menuPause.DisActive();
        GamePaused = false;
        Time.timeScale = 1f;
    }

    public static bool verifierEncadrement(float[] liste, float num)
    {
        if(liste.Length != 2)
        {
            Debug.LogError("Mauvais encadrement");
            return false;
        }
        return num >= liste[0] && num <= liste[1];
    }

    public static void PlayAudioAndDie(Transform parent, AudioClip song, float spaceSong = 1, bool TransformIsParent = true)
    {
        GameObject temp = new GameObject("tempAudio");
        if (TransformIsParent)
        {
            temp.transform.parent = parent;
            temp.transform.localPosition = Vector3.zero;
        }
        else
        {
            temp.transform.position = parent.position;
        }        
        Physics.SyncTransforms();
        temp.AddComponent<AudioSource>();
        AudioSource source = temp.GetComponent<AudioSource>();
        source.enabled = true;
        source.playOnAwake = false;
        temp.SetActive(true);
        source.clip = song;
        source.loop = false;
        source.spatialBlend = spaceSong;
        source.Play();
        Destroy(temp,source.clip.length);
    }

    public static void BackToSpawn(GameObject player,Vector3 spawn)
    {       

        player.transform.position = spawn;
        Physics.SyncTransforms();
        var rb = player.GetComponent<Rigidbody>();
        var collider = player.GetComponent<BoxCollider>();
        var bottomPlayer = collider.bounds.min;
        //bottommPlayer.y : hauteur entre le sol et le bas du personnage, pour faire le personnage être posez sur le sol, il faut ajouter cette hauteur (positive)
        Debug.Log(player.transform.position + " | " + collider.bounds.min);
        player.transform.position = player.transform.position + new Vector3(0, -bottomPlayer.y, 0);
        RaycastHit hit;
        Debug.Log("in");
        if (Physics.Raycast(player.transform.position,player.transform.forward,out hit, 100f))
        {
            Debug.Log("detected " + hit.collider.name);
            Debug.DrawLine(player.transform.position, hit.point);
            if(hit.collider.gameObject.name == "SpawnHouse")
            {
                Debug.Log("rotate");
                player.transform.Rotate(0, 180, 0);
            }
        }
    }

    public static void ConvertAngle(Vector3 angle, out Vector3 rotation)
    {
        float x = (float)angle.x;
        float y = (float)angle.y;
        float z = (float)angle.z;

        float[] rota = {x, y, z };
        for(int i = 0; i < rota.Length; i++)
        {
            if (rota[i] > 180)
            {
                rota[i] -= 360;
            }
        }
        rotation = new Vector3(rota[0], rota[1], rota[2]);
    }

    public static void ActiveCursorForGun(Texture2D cursor,Vector2 sourisPoint)
    {
        Cursor.SetCursor(cursor, sourisPoint, CursorMode.Auto);

    }

    public static void BackToNormalCursor()
    {
        Cursor.SetCursor(null,new Vector2(0,0),CursorMode.Auto);
        
    }

    public static IEnumerator effaceTextIn(TextMeshProUGUI text,float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = "";
    }

    public static IEnumerator effaceTextIn(TextMeshPro text, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = "";
    }

    public static bool calculDistance(Vector3 point1, Vector3 point2)
    {
        Vector3 vecteur_distance = point1 - point2;
        return vecteur_distance.magnitude <= 10;
    }

    public static void playSongAt(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }


}
