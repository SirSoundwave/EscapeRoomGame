using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{

    private bool z_Interacted = false;
    public GameEvent InteractionEvent;
    public int data = 0;

    protected override void OnCollided(GameObject collidedObject)
    {
        //base.OnCollided(collidedObject);

        if (Input.GetKey(KeyCode.E))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        if (!z_Interacted)
        {
            z_Interacted = true;
            Debug.Log("Interacted with " + data);
            InteractionEvent.Raise(this, data);
        }
    }
}
