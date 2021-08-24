using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSequence : MonoBehaviour
{
    private ISequence _activeSequence;

    //public ISequence GetSequence => _activeSequence;
    public ISequence GetSequence()
    {
        foreach (var e in _activeSequence.Sequence)
        {
            Debug.Log(e);
        }
        return _activeSequence;
    }

    public OverrideSequence overRideSequence;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ApplySequence(ISequence currentSequence)
    {
        _activeSequence = currentSequence;

        if (overRideSequence != null)
        {
            _activeSequence.Sequence = new float[overRideSequence.overrideSequence.Length];
            _activeSequence.Sequence = overRideSequence.overrideSequence;            
        }
        
      
    
    }


}
