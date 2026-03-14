using System.Collections.Generic;
using UnityEngine;

public class Unlockdoor : MonoBehaviour
{
    [SerializeField] private Doorscr[] doors; // assign doors in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Doorscr d in doors)
            {
                d.UnlockDoor(); // use permanent unlock if you want
            }
        }
    }
}
