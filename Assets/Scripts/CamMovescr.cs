using UnityEngine;
using UnityEngine.Rendering;

public class CamMovescr : MonoBehaviour
{
    private Transform end;
    private bool moving;
    private Vector3 v = Vector3.zero;
    private float sV = 0f;
    [SerializeField] float time = .4f;
    private float size;
    Camera c;
    public void moveCam(Transform T, float s)
    {
        Debug.Log("in movecam");
        end = T;
        size = s;
        moving = true;
    }
    void Awake()
    {
        moving = false;
    }
    private void Start()
    {
        c = GetComponent<Camera>();
    }

    void Update()
    {
        if (!moving)
        {
            return;
        }
        //Debug.Log("moving");
        Vector3 e = end.position;
        e.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, e, ref v, time);
        c.orthographicSize = Mathf.SmoothDamp(c.orthographicSize, size, ref sV, time);
        if (Vector3.Distance(transform.position, e) < 0.01f)
        {
            moving = false;
        }//if
    }//update
}//end
