using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInfoUpdater : MonoBehaviour
{
    public PuzzleInfo puzzleInfo;

    public void MarkCompleteByIndex(int index)
    {
        UpdateByIndex(index, true);
    }

    public void MarkCompleteByName(string name)
    {
        UpdateByName(name, true);
    }

    public void UpdateByIndex(int index, bool completed)
    {
        this.puzzleInfo.puzzles[index].completed = completed;
    }

    public void UpdateByName(string name, bool completed)
    {
        PuzzleBlock result = this.puzzleInfo.puzzles.Find(delegate(PuzzleBlock block) {
            return block.puzzleName == name;
        });

        if(result != null)
        {
            result.completed = completed;
        }
    }

}
