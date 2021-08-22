using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attatch this script to the "Crab_image" objects within the hierarchy.
public class Crabs : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private bool _crabHasBeenSelected;

    private void Start()
    {
        StartCoroutine(HiddenCrabAnimations());
    }

    private IEnumerator HiddenCrabAnimations()
    {
        while (_crabHasBeenSelected == false)
        {
            yield return new WaitForSeconds(7.0f);
            int randomAnimation = Random.Range(0, 4);
            Debug.Log(randomAnimation);
            
            switch (randomAnimation)
            {
                case 0:
                    _anim.SetBool("HideLooking", true);
                    yield return new WaitForSeconds(.5f);
                    _anim.SetBool("HideBlink", false);
                    break;
                case 1:
                    _anim.SetBool("HideBlink", true);
                    yield return new WaitForSeconds(.5f);
                    _anim.SetBool("HideBlink", false);
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }

    public void ClickedOnCrab()
    {
        _crabHasBeenSelected = true;
        _anim.SetBool("CrabHasBeenSelected", true);
        //reveal next winnings ammount in the sequence.
        //Update Winnings in UIManager.
    }
}
