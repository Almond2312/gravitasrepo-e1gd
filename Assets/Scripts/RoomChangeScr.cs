using TMPro;
using UnityEngine;

public class RoomChangeScr : MonoBehaviour
{
    public Transform end;//empty object in the editor to show place for camera to go
    private CamMovescr c;
    [SerializeField] float size;
    //there are multiple roomchangers, so each one only needs one camerachange rectangle each with its own end transform
    private void Start()
    {
        c = Camera.main.GetComponent<CamMovescr>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("call movecam");
            c.moveCam(end, size);
        }//if
    }//triggerenter
}//end