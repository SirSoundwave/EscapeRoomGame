using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleCompletionCapsule
{
    public GameEvent channel;
    public int data;
}

public class PuzzleCompletionManager : DataManager
{
    public PuzzleInfo puzzleInfo;

    public List<PuzzleCompletionCapsule> signals;

    public void ResetData()
    {
        for (int i = 0; i < puzzleInfo.puzzles.Count; i++)
        {
            puzzleInfo.puzzles[i].completed = false;
        }
    }

    public override void LoadData()
    {
        for (int i = 0; i < puzzleInfo.puzzles.Count; i++)
        {
            if (puzzleInfo.puzzles[i].completed)
            {
                signals[i].channel.Raise(this, signals[i].data);
            }
        }
    }

    public override void SaveData()
    {
        //unused
    }

}
