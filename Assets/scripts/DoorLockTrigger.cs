using UnityEngine;
using System.Collections.Generic;

public class DoorLockTrigger : MonoBehaviour
{
    [SerializeField] private List<Doorscr> doors;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Doorscr door in doors)
            {
                door.LockDoor();
            }
        }
    }
}