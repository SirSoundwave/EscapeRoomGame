using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneExit : MonoBehaviour
{
    public List<DataManager> managers;

    private bool exitComplete = false;

    public void OnExit()
    {
        foreach (DataManager manager in managers)
        {
            manager.SaveData();
        }

        exitComplete = true;
    }

    public bool ExitCompleted()
    {
        return exitComplete;
    }
}
