using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorData
{
    public string name;
    public Vector3 position;
    public bool opened;
}

public class Door : MonoBehaviour
{

    public float speed = 5f;

    public GameObject endPos;

    [HideInInspector]
    public bool opened = false;

    public void Update()
    {
        if (opened && this.transform.position != endPos.transform.position)
        {
            this.transform.position = Vector3.Lerp(transform.position, endPos.transform.position, speed * Time.deltaTime);
        }
    }

    public void Open()
    {
        if (!opened)
        {
            opened = true;
        }
    }
}
