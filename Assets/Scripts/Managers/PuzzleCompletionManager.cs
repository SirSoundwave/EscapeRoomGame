using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleCompletionCapsule
{
    public GameEvent channel;
    public int data;
}

public class PuzzleCompletionManager : MonoBehaviour
{
    public PuzzleInfo puzzleInfo;

    public List<PuzzleCompletionCapsule> signals;

    private void Start()
    {
        for(int i = 0; i < puzzleInfo.puzzles.Count; i++)
        {
            if (puzzleInfo.puzzles[i].completed)
            {
                signals[i].channel.Raise(this, signals[i].data);
            }
        }
    }

}
