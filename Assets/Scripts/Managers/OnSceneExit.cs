using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneExit : MonoBehaviour
{
    public List<DataManager> managers;

    public void OnExit()
    {
        foreach (DataManager manager in managers)
        {
            manager.SaveData();
        }
    }
}
