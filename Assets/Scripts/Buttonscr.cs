using UnityEngine;

public class Buttonscr : MonoBehaviour
{
    [SerializeField] private bool isToggle;
    [SerializeField] private int soloId;
    [SerializeField] private int groupId;
    [SerializeField] private int internalGroupId;
    private bool on;
    private Animator a;
    private int touching;

    private void Awake()//Awake is for initialization involving only self referential parts. Start is safer to run things 
    {//that reference other objects. Like say the button wanted to get a block object, maybe by chance the block is made
    //after the button, the button wouldnt create right. In start, everything is guaranteed to at least be created already.
        a = GetComponent<Animator>();
        on = false;
        visualUpdate();
    }//This only references things in this script or attached to button, so it belongs in awake.

    private void visualUpdate()
    {
        a.SetBool("On", on);
        a.SetBool("IsToggle", isToggle);//the first one references the name in the parameter, the second is the script variable
    }

    public void setState(bool state)
    {
        on = state;
        visualUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touching++;
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Pushblock")) && touching == 1)
        {//i want this to run when something new touches the block, but now when the thing is already touched and something new touches it
            //so like if block is touching, and player touches again, i dont want a second toggle
            if (isToggle)
            {
                setState(!on);
            }
            else
            {
                setState(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touching--;
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Pushblock")) && touching == 0)
        {
            if (!isToggle)
            {
                setState(false);
            }
        }
    }

    public bool getState()
    {
        return on;
    }

    void Update()
    {
        Debug.Log(isToggle + " " + soloId + " " + groupId + " " + on + " " + touching);
    }
}
