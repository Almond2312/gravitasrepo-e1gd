using UnityEngine;

public class WinTriggerscr : MonoBehaviour
{
    [SerializeField] private GameObject winMenu;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggerhit");
        if (other.CompareTag("Player"))
        {
            winMenu.SetActive(true);
        }
    }
}
