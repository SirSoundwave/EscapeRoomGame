using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    public List<DataManager> managers;

    private void Start()
    {
        foreach(DataManager manager in managers){
            manager.LoadData();
        }
    }
}
