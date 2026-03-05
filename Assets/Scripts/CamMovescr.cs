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
    [SerializeField] float targetAspect = 16f / 9f;
    public void moveCam(Transform T, float s)
    {
        //Debug.Log("in movecam");
        end = T;
        size = s;
        moving = true;
    }
    void Awake()
    {
        moving = false;
    }
    void SetAspectRatio()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = c.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            c.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = c.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            c.rect = rect;
        }
    }
    private void Start()
    {
        c = GetComponent<Camera>();
        SetAspectRatio();
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
