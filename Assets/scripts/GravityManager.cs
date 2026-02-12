using UnityEngine;

public class GravityManager : MonoBehaviour
{
    //black magic
    public static GravityManager Instance;

    public Vector2 gravity = Vector2.down;

    [SerializeField] private float gravStrength = -27f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //more black magic
        Physics2D.gravity = gravity * gravStrength;
    }

    public void setGravity(Vector2 dir)
    {
        gravity = dir;
        Physics2D.gravity = gravity * gravStrength;
    }

}
