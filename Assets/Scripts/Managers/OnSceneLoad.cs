using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEventCapsule
{
    public GameEvent ev;
    public string data;
}

public class OnSceneLoad : MonoBehaviour
{
    public List<DataManager> managers;

    public CustomGameEvent startMethods;

    public List<GameEventCapsule> startEvents;

    private void Start()
    {
        foreach(DataManager manager in managers){
            manager.LoadData();
        }
        foreach(GameEventCapsule cap in startEvents)
        {
            cap.ev.Raise(this, cap.data);
        }
        startMethods.Invoke(this, null);
    }
}
