using Unity.VisualScripting;
using UnityEngine;

public class gravchangerscr : MonoBehaviour
{
    [SerializeField] private float cutcorner = .15f;
    private bool touching = false;
    Collider2D player;
    //private bool once = false;

    public void gravChange(Vector2 dir)
    {
        GravityManager.Instance.setGravity(dir);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.GetComponent<Collider2D>().CompareTag("Player"))
        {
            return;
        }
        //once = false;
        touching = true;
        player = other;
    }//ontrigger

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<Collider2D>().CompareTag("Player"))
        {
            return;
        }
        touching = false;
    }
    private void Update()
    {
        //grav change is done in update because it needs to run repeatedly to ignore corners.
        if (!touching)
        {
            return;
        }
        Vector2 d = player.transform.position - transform.position;
        
        //if (!once)
        //{
            //Debug.Log(d + " " + (Mathf.Abs(d.x) - Mathf.Abs(d.y)) + " " + (Mathf.Abs((Mathf.Abs(d.x) - Mathf.Abs(d.y))) <= cutcorner));
        //}bugfixing

        //ignore corners. approaching from side will have a larger difference between x and y than cutcorner.
        //cutcorner cuts nothing at 0, and stops block function at 1
        if (Mathf.Abs((Mathf.Abs(d.x) - Mathf.Abs(d.y))) <= cutcorner)
        {
            return;
        }

        //since it isn't possible to be inside the grav changer, the only way for d.x or d.y to be <1 is if the player is not approaching from that side
        //say aligned exactly on the x axis but then exactly 1 away on the y. thus is d.y > d.x, the player is approaching from y. the larger vector portion
        //is the direction of approach. this does break a bit if hit from the corner though. here it just defaults to topside if at corner
        if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
        {
            //now that it is set that the approach is from the x axis, just check the sign and that will be left or right.
            if (d.x > 0)
            {
                //Debug.Log("L");
                gravChange(Vector2.left);//ai actually got this part wrong, gravity needs to be the opposite of approach not same :)
            }
            else
            {
                //Debug.Log("R");
                gravChange(Vector2.right);
            }
        }
        else if (Mathf.Abs(d.x) < Mathf.Abs(d.y))
        {
            if (d.y > 0)
            {
                //Debug.Log("D");
                gravChange(Vector2.down);
            }
            else
            {
                //Debug.Log("U");
                gravChange(Vector2.up);
            }//ifelse2
        }//ifelse1
    }//update
}//total end
