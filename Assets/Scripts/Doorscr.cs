using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Doorscr : MonoBehaviour
{
    [SerializeField] private int OneId;
    public List<int> MultiId;//apparently public lists dont need to be serialized fields
    private Dictionary<int, bool> buttonStates;
    private Animator a;
    private bool open;
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        a = GetComponent<Animator>();
        open = false;
        buttonStates = new Dictionary<int, bool>();
        if (OneId != 0 && !buttonStates.ContainsKey(OneId))
        {
            buttonStates.Add(OneId, false);
        } 
        else if (OneId == 0)
        {
            foreach (int id in MultiId)
            {
                if (!buttonStates.ContainsKey(id))
                {
                    buttonStates.Add(id, false);
                }//if
            }//foreach
        }//elif
    }//awake

    private void OnEnable()
    {
        Buttonscr.ButtonPressed += ButtonPressed;
    }

    private void OnDisable()
    {
        Buttonscr.ButtonPressed -= ButtonPressed;
    }//ai tells me that for events you want the stuff to be in the enables.

    private void ButtonPressed(int id, bool state)
    {
        if (buttonStates.ContainsKey(id))
        {
            buttonStates[id] = state;
            updateOpen();
            Debug.Log("Door " + gameObject.name + " received button press from button " + id + " with state " + state);
        }
    }

    private void updateOpen()
    {
        bool newOpen = true;
        foreach (bool state in buttonStates.Values)
        {
            if (!state)
            {
                newOpen = false;
                break;
            }
        }
        if (newOpen != open)
        {
            open = newOpen;
            a.SetBool("Open", open);//open in both the editor and script can be true or false, so this is also responsible for close animation.
            //Debug.Log("Door " + gameObject.name + " is now " + (open ? "open" : "closed"));
            col.enabled = !open;
        }//if
    }//updateOpen
}