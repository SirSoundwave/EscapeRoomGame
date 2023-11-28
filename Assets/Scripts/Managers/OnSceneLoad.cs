using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    public CustomGameEvent loadEvents;

    private void Start()
    {
        loadEvents.Invoke(this, null);
    }
}
