using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBehavior : MonoBehaviour, ISequence
{
    [SerializeField] private float[] _sequence;
    public float[] Sequence { get; set; }
  
    
    private ActiveSequence _activeSequence;

    private void Start()
    {
        Sequence = new float[_sequence.Length];

        Sequence = _sequence;

        //sequence = _sequence;
        _activeSequence = GameObject.FindObjectOfType<ActiveSequence>();

    }

    public void OnSelected()
    {
        _activeSequence.ApplySequence(this); 
    }
}
