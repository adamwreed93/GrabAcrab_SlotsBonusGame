using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ISequence _activeSequence;
    public ISequence GetActiveSequence => _activeSequence;

    private void Awake()
    {
        //Locates the active sequence 
        _activeSequence = GameObject.Find("ActiveSequence").GetComponent<ActiveSequence>().GetSequence();

        if (_activeSequence == null)
        {
            Debug.LogError("ActiveSequence is NULL!");
        }
        else
        {
            Destroy(GameObject.Find("ActiveSequence"));
        }
    }
}
