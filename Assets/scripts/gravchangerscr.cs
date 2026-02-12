using UnityEngine;

public class gravchangerscr : MonoBehaviour
{
    public void gravChange(Vector2 dir)
    {
        GravityManager.Instance.setGravity(dir);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        //'other' at this point is the player. the transformposition of the player - of the grav changer gives vector from block to player
        Vector2 d = other.transform.position - transform.position;

        //since it isn't possible to be inside the grav changer, the only way for d.x or d.y to be <1 is if the player is not approaching from that side
        //say aligned exactly on the x axis but then exactly 1 away on the y. thus is d.y > d.x, the player is approaching from y. the larger vector portion
        //is the direction of approach. this does break a bit if hit from the corner though. here it just defaults to topside if at corner
        if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
        {
            //now that it is set that the approach is from the x axis, just check the sign and that will be left or right.
            if (d.x > 0)
            {
                gravChange(Vector2.left);//ai actually got this part wrong, gravity needs to be the opposite of approach not same :)
            }
            else
            {
                gravChange(Vector2.right);
            }
        }
        else
        {
            if (d.y > 0) 
            {
                gravChange(Vector2.down);
            }
            else
            {
                gravChange(Vector2.up);
            }//ifelse2
        }//ifelse1
    }//ontrigger
}
