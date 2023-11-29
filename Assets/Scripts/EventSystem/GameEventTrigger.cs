using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField]
    private string tagCheck;
    [SerializeField]
    private GameEvent triggerEvent;
    [SerializeField]
    private int data;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagCheck))
        {
            triggerEvent.Raise(this, data);
        }
    }

    public void RaiseEvent()
    {
        triggerEvent.Raise(this, data);
    }
}
