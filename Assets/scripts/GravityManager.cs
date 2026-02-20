using UnityEngine;

public class GravityManager : MonoBehaviour
{
    //for the awake function
    public static GravityManager Instance;
    //public static event System.Action<Vector2> OnGravityChanged;


    public Vector2 relativeUp;
    public Vector2 relativeDown;
    public Vector2 relativeLeft;
    public Vector2 relativeRight;

    [SerializeField] private float gravStrength = -27f;
    [SerializeField] public float tv = 20f;

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
    }

    private void Start()
    {
        //instantiation after awake to ensure that the gravmanager exists before values are set
        relativeDown = Vector2.down;
        relativeUp = Vector2.up;
        relativeLeft = Vector2.left;
        relativeRight = Vector2.right;
        Physics2D.gravity = relativeDown * gravStrength;
    }

    public void setGravity(Vector2 dir)
    {
        dir = dir.normalized;//sometimes funny business happens.
        relativeDown = dir;//proud i came up with public relative directions will be super useful
        relativeUp = -dir;
        relativeLeft = -Vector2.Perpendicular(dir).normalized;
        relativeRight = -relativeLeft;
        Physics2D.gravity = relativeDown * gravStrength;
    }

    public static Vector2 getVComp(Vector2 linearV)
    {
        return GravityManager.Instance.relativeDown * Vector2.Dot(linearV, GravityManager.Instance.relativeDown);
    }

    public static Vector2 getHComp(Vector2 linearV)
    {
        return GravityManager.Instance.relativeRight * Vector2.Dot(linearV, GravityManager.Instance.relativeRight);
    }

    public static Vector2 terminalV(Vector2 linearV)
    {
        if (getVComp(linearV).magnitude > GravityManager.Instance.tv)
        {
            return getHComp(linearV) + (GravityManager.Instance.relativeDown * GravityManager.Instance.tv);
        }
        return linearV;
    }
}
