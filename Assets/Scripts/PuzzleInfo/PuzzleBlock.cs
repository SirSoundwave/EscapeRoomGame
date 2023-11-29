using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleBlock
{
    public string puzzleName;
    public bool completed;

    public PuzzleBlock(string puzzleName, bool completed)
    {
        this.puzzleName = puzzleName;
        this.completed = completed;
    }
}
