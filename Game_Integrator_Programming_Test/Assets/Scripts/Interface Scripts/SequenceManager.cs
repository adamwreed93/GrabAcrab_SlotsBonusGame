using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is to be attatched to a prefab button object in order to easily add a new sequence from the inspector. (AR)
public class SequenceManager : MonoBehaviour, ISequences
{
    public float[] NewSequence { get; set; }

    [SerializeField] private float[] _sequence;


    public void CallThisSequenceWhenClicked()
    {
        NewSequence = new float[_sequence.Length];

        Debug.Log("Checkpoint");
        Debug.Log("the sequence length is " + _sequence.Length);

        for (int i = 0; i < _sequence.Length; i++)
        {
            NewSequence[i] = _sequence[i];
        }
        Debug.Log(NewSequence.Length);
    }
}
