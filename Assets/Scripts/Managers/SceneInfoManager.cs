using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoManager : DataManager
{

    public SceneInfo info;

    public GameObject player;

    public DoorManager DoorManager;

    public override void LoadData()
    {
        player.transform.position = info.playerPosition;
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
        
        info.playerPosition = player.transform.position;
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
