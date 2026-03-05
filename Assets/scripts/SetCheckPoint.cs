using UnityEngine;
using UnityEngine.Rendering;

public class SetCheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerscr player = other.GetComponent<playerscr>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
            }
        }
    }
}
