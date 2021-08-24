using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is to be attatched to each "Sequence Button" in the main menu.
public class SequenceBehavior : MonoBehaviour, ISequence
{
    public float[] Sequence { get; set; } //Part of the "ISequence" interface.
    [SerializeField] private float[] _sequence;
    
    private ActiveSequence _activeSequence;


    private void Start()
    {
        Sequence = new float[_sequence.Length];
        Sequence = _sequence;

        _activeSequence = GameObject.FindObjectOfType<ActiveSequence>();
    }

    public void OnSelected()
    {
        _activeSequence.ApplySequence(this); 
    }
}
