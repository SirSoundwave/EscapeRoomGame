using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : InteractableObject
{
    public PuzzleInfo info;
    public int AssociatedPuzzleIndex;

    protected override void Start()
    {
        base.Start();
        Debug.Log(info.puzzles[AssociatedPuzzleIndex].puzzleName + " complete: " + info.puzzles[AssociatedPuzzleIndex].completed);
        if (info.puzzles[AssociatedPuzzleIndex].completed)
        {
            this.z_Interacted = true;
        }
    }

}
