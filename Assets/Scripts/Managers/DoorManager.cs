using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [HideInInspector]
    public Door[] doors;

    private void Awake()
    {
        doors = GetComponentsInChildren<Door>();
    }

    public void OpenDoor(Component sender, object data)
    {
        if(data is int)
        {
            doors[(int)data].Open();
        }
    }

}
