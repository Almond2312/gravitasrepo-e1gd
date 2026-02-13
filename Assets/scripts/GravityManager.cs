using UnityEngine;

public class GravityManager : MonoBehaviour
{
    //for the awake function
    public static GravityManager Instance;
    //public static event System.Action<Vector2> OnGravityChanged;


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
        }//make sure there is never more than one gravity manager
        Physics2D.gravity = gravity * gravStrength;
    }

    public void setGravity(Vector2 dir)
    {
        gravity = dir;
        Physics2D.gravity = gravity * gravStrength;
    }

}
