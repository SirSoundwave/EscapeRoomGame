using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    public GameEvent potionChannel;

    private Animator anim;
    private bool filled = true;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void UsePotion() {
        filled = false;
        anim.SetTrigger("use");
        Debug.Log("Used " + name);
        potionChannel.Raise(this, name);        
    }

    public void TriggerRefill()
    {
        if (!filled)
        {
            Debug.Log("Filled " + name);
            anim.SetTrigger("refill");
            filled = true;
        }
    }
}
