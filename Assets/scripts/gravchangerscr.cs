using UnityEngine;

public class gravchangerscr : MonoBehaviour
{
    public void gravChange(Vector2 dir)
    {
        GravityManager.Instance.setGravity(dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.CompareTag("Player"))
        {
            return;
        }

        //'collision' at this point is the player. the get contact(0) is the first point where the player hits the grav changer.
        //the normal is the unit vector perpendicular direction between them
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 d = contact.normal;


        //since it isn't possible to be inside the grav changer, the only way for d.x or d.y to be <1 is if the player is not approaching from that side
        //say aligned exactly on the x axis but then exactly 1 away on the y. thus is d.y > d.x, the player is approaching from y. the larger vector portion
        //is the direction of approach. this does break a bit if hit from the corner though. here it just defaults to topside if at corner
        if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
        {
            //now that it is set that the approach is from the x axis, just check the sign and that will be left or right.
            if (d.x > 0)
            {
                gravChange(Vector2.right);//ai actually got this part wrong, gravity needs to be the opposite of approach not same :)
            }
            else
            {
                gravChange(Vector2.left);
            }
        }
        else if (Mathf.Abs(d.x) < Mathf.Abs(d.y))
        {
            if (d.y > 0)
            {
                gravChange(Vector2.up);
            }
            else
            {
            gravChange(Vector2.down);
            }//ifelse2
        }//ifelse1
    }//ontrigger
}
