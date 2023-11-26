using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Door[] doors;

    private void Start()
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
