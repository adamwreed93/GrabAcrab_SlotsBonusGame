using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sequence", menuName = "Create New Sequence")]

public class Sequences : ScriptableObject
{
    public float[] Sequence;
    public bool SequenceIsActive;
}
