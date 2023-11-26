using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Info", menuName = "Escape Room/Puzzle Info", order = 0)]
public class PuzzleInfo : ScriptableObject
{
    [SerializeField]
    public List<PuzzleBlock> puzzles = new List<PuzzleBlock>();
}
