using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCodedGameEventListener : GameEventListener
{
    public int data;

    public override void OnEventRaised(Component sender, object data)
    {
        if (data is int && ((int)data).Equals(this.data))
            response.Invoke(sender, data);
    }
}
