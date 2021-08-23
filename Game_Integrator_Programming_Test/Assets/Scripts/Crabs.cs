using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Attatch this script to the "Crab_image" objects within the hierarchy.
public class Crabs : MonoBehaviour
{
    UIManager uiManager;
    [SerializeField] private GameObject _uiManager;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _winningsAmmountTextObject;
    [SerializeField] private Text _winningsAmmountText;
    [SerializeField] private GameObject _youWonSign;
    [SerializeField] private GameObject _alreadySelectedCrabContainer; //This will seperate the crabs that have already been picked so that when the countdown reaches "0" it will be able to pick a random new crab.
    [SerializeField] private Button _crabButton;
    private bool _crabHasBeenSelected; //This is marked as true as soon as the crab associated with this script has been selected.
    public static bool _canSelectCrab = true; //This is marked true when you can select a crab and becomes false for a short ammount of time after a crab is selected. This is "Static" and thus is shared between all instances of this class(script).
    private float _nextAmmountInSequence; //This is the next ammount to be displayed when a crab is selected.
    public static int _numberOfCrabsSelected = -1; //This tracks how many crabs have been picked and thus which number we are on in the sequence array. It starts at -1 so that as soon as you select your first crab it becomes the 0 object in the array.
    private bool _isGameOver; //Becomes true after you finish the sequence of winnings in the bonus game.



    private void Start()
    {
        uiManager = _uiManager.GetComponent<UIManager>();
        StartCoroutine(HiddenCrabAnimations());

        if (uiManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }
    }


    private IEnumerator HiddenCrabAnimations() //This plays the idle animations for the crabs while they have yet to be selected.
    {
        while (_crabHasBeenSelected == false)
        {
            int randomWaitTime = Random.Range(5, 20);
            yield return new WaitForSeconds(randomWaitTime);
            int randomAnimation = Random.Range(0, 4);
            
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

    public void ClickedOnCrab() //This is called when the player selects a crab during the bonus game.
    {
        if (_canSelectCrab == true && _numberOfCrabsSelected < uiManager._currentWinningSequence.Length -1 && _isGameOver == false)
        {
            _canSelectCrab = false;
            uiManager.TurnCrabButtonsOff();
            _numberOfCrabsSelected++;
            _crabHasBeenSelected = true;
            _anim.SetBool("CrabHasBeenSelected", true);
            transform.SetParent(_alreadySelectedCrabContainer.transform);
            _crabButton.interactable = false;
            StartCoroutine(UpdateNextWinningsText());
            StartCoroutine(CrabSelectCooldown());
        }
    }

    private IEnumerator CrabSelectCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        _winningsAmmountTextObject.SetActive(true);
        uiManager.UpdateCrabCount(_numberOfCrabsSelected);
        yield return new WaitForSeconds(1.0f);
        if (_isGameOver == false)
        {
            _canSelectCrab = true;
            uiManager.TurnCrabButtonsOn();
        }
    }

    public IEnumerator UpdateNextWinningsText()
    {
        if (_numberOfCrabsSelected < uiManager._currentWinningSequence.Length - 1)
        {
            _nextAmmountInSequence = uiManager._currentWinningSequence[_numberOfCrabsSelected];
            _winningsAmmountText.text = "$" + _nextAmmountInSequence + ".00";
        }
        else if (_numberOfCrabsSelected == uiManager._currentWinningSequence.Length - 1)
        {
            _nextAmmountInSequence = uiManager._currentWinningSequence[_numberOfCrabsSelected];
            _winningsAmmountText.text = "$" + _nextAmmountInSequence + ".00";
            yield return new WaitForSeconds(2.0f);
            _youWonSign.SetActive(true);
            _isGameOver = true;
        }
    }
}
