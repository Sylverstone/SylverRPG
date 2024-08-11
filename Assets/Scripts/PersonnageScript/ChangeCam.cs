using UnityEngine;

public class ChangeCam : MonoBehaviour
{

    public Camera[] cams;
    public int currentCamIndex = 0;

    private void Awake()
    {
        cams = GetComponentsInChildren<Camera>();

        Debug.Log(cams.Length);
        foreach (var cameras in cams)
        {
            cameras.gameObject.SetActive(false);
        } 
        cams[currentCamIndex].gameObject.SetActive(true);
    }
    // Start is called before the first frame update

    public void changeCam()
    {
        Debug.Log("changeCam");
        cams[currentCamIndex].gameObject.SetActive(false);
        currentCamIndex += 1;
        currentCamIndex %= cams.Length;
        cams[currentCamIndex].gameObject.SetActive(true) ;
        cams[currentCamIndex].enabled = true;
        cams[currentCamIndex].targetDisplay = 0;
    }

    public void chooseCam(int index = 0)
    {
        if (index < cams.Length)
        {
            cams[currentCamIndex].gameObject.SetActive(false);
            currentCamIndex = index;
            cams[currentCamIndex].gameObject.SetActive(true);
            cams[currentCamIndex].enabled = true;
            cams[currentCamIndex].targetDisplay = 0;
        }
    }
}
