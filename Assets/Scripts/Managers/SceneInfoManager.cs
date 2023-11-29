using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoManager : DataManager
{

    public SceneInfo info;
    public SceneInfo defaultInfo;

    public bool resetOnStart = false;
    private bool resetComplete = false;

    public GameObject player;

    public DoorManager DoorManager;

    private void Awake()
    {
        if (resetOnStart)
        {
            ResetData();
            
        }
    }

    public bool GetResetComplete()
    {
        return resetComplete;
    }

    public void ResetData()
    {
        PlayerPrefs.SetFloat("PlayerX", defaultInfo.playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", defaultInfo.playerPosition.y);
        info.doors = new List<DoorData>();
        resetComplete = true;
        //Debug.Log("Data reset");
    }

    public override void LoadData()
    {
        Vector3 position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), 0);
        player.transform.position = position;
        //Debug.Log("Loaded position: " + info.playerPosition.x + ", " + info.playerPosition.y);
        //Debug.Log("New loaded position: " + player.transform.position.x + ", " + player.transform.position.y);
        if (info.doors != null)
        {
            //Debug.Log("Found Doors to load");
            //Debug.Log("Door Manager contains " + DoorManager.doors.Length + " doors");
            for (int i = 0; i < DoorManager.doors.Length; i++)
            {
                //Debug.Log("Finding door " + DoorManager.doors[i].name);
                DoorData requested = info.doors.Find(delegate (DoorData data)
                {
                    return data.name.Equals(DoorManager.doors[i].name);
                });
                if (requested != null)
                {
                    //Debug.Log("Loading door " + requested.name);
                    DoorManager.doors[i].transform.position = requested.position;
                    DoorManager.doors[i].opened = requested.opened;
                }

            }
        }
    }

    public override void SaveData()
    {

        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //Debug.Log("Saved position: " + player.transform.position.x + ", " + player.transform.position.y);
        //Debug.Log("New saved position: " + info.playerPosition.x + ", " + info.playerPosition.y);
        info.doors = new List<DoorData>();
        for(int i = 0; i < DoorManager.doors.Length; i++)
        {
            DoorData temp = new DoorData();
            temp.name = DoorManager.doors[i].name;
            temp.opened = DoorManager.doors[i].opened;
            temp.position = DoorManager.doors[i].transform.position;
            info.doors.Add(temp);
        }
        //Debug.Log("Finished Saving Doors");
    }

}
