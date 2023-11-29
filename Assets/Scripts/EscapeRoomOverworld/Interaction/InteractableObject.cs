using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{

    protected bool z_Interacted = false;
    public GameEvent InteractionEvent;
    public int data = 0;

    public Animator tooltipAnim;

    private bool colliding = false;
    private bool tooltipVisible = false;

    protected override void OnCollided(GameObject collidedObject)
    {
        //base.OnCollided(collidedObject);
        //Debug.Log("collided with " + name);
        colliding = true;
        if (Input.GetKey(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected override void Update()
    {
        colliding = false;
        base.Update();

        if (colliding && !tooltipVisible)
        {
            tooltipVisible = true;
            tooltipAnim.SetTrigger("FadeIn");
        } else if(!colliding && tooltipVisible)
        {
            tooltipVisible = false;
            tooltipAnim.SetTrigger("FadeOut");
        }
    }

    private void OnInteract()
    {
        //Debug.Log("Interacted: " + z_Interacted);
        if (!z_Interacted)
        {
            z_Interacted = true;
            //Debug.Log("Interacted with " + name);
            InteractionEvent.Raise(this, data);
        }
    }
}
