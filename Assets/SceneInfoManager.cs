using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoManager : MonoBehaviour
{

    public SceneInfo info;

    public GameObject player;

    private void Awake()
    {
        player.transform.position = info.playerPosition;
    }

    public void SaveData()
    {
        Debug.Log("Saving position: " + player.transform.position.x + ", " + player.transform.position.y);
        info.playerPosition = player.transform.position;
    }

}
