using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataManager : MonoBehaviour
{
    public abstract void SaveData();

    public abstract void LoadData();
}
