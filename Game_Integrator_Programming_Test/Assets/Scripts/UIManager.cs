using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour//, ISequences
{
    //public float[] NewSequence { get; set; }


    [SerializeField] private GameObject _openingTransition;
    [SerializeField] private GameObject _endingTransition;
    [SerializeField] private Transform _crabContainer;
    [SerializeField] private GameObject _countdownTextObject;
    [SerializeField] private Text _countdownText;
    [SerializeField] private Text _totalWinningsSignText;
    [SerializeField] private Text _youWonSignWinningsText;
    private float _totalWinningsAmmount;
    [SerializeField] private int _numberOfCrabsPicked = -1;
    [SerializeField] private int _crabNumberChecker = -1;
    [SerializeField] private int _countdownCurrentTime = 20;
    
    private ISequence _activeSequence;
    public ISequence GetActiveSequence => _activeSequence;

    private void Awake()
    {

        //locate the active sequence 
        _activeSequence = GameObject.Find("ActiveSequence").GetComponent<ActiveSequence>().GetSequence;

    }
    private void Start()
    {
        StartCoroutine(DisableTransitionAnimation());
        StartCoroutine(PickACrabCountdown());
    }

    private void Update()
    {
        if (_countdownTextObject != null)
        {
            _countdownText.text = _countdownCurrentTime.ToString();
        }
    }

    public void UpdateCrabCount(int crabCount)
    {
        _numberOfCrabsPicked = crabCount;
        _totalWinningsAmmount += _activeSequence.Sequence[_numberOfCrabsPicked];
        _totalWinningsSignText.text = "$" + _totalWinningsAmmount + ".00";
        _youWonSignWinningsText.text = "$" + _totalWinningsAmmount + ".00";
    }

    private IEnumerator PickACrabCountdown()
    {
        while (_numberOfCrabsPicked != _activeSequence.Sequence.Length - 1)
        {
            if (_countdownCurrentTime == 0)
            {
                int NumberOfCrabsLeft = _crabContainer.childCount;
                int RandomCrab = Random.Range(0, NumberOfCrabsLeft);
                Debug.Log("You Picked Crab Number " + RandomCrab);
                _crabContainer.GetChild(RandomCrab).GetComponent<Crabs>().ClickedOnCrab();
                _countdownCurrentTime = 20;
            }

            if (_countdownCurrentTime <= 10)
            {
                _countdownTextObject.SetActive(true);
            }
            else
            {
                _countdownTextObject.SetActive(false);
            }

            if (_crabNumberChecker == _numberOfCrabsPicked)
            {
                yield return new WaitForSeconds(1.0f);
                _countdownCurrentTime--;
            }
            else if (_crabNumberChecker != _numberOfCrabsPicked)
            {
                _countdownCurrentTime = 20;
                _crabNumberChecker = _numberOfCrabsPicked;
            }
        }
        _countdownTextObject.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        _endingTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);

    }

    private IEnumerator DisableTransitionAnimation()
    {
        yield return new WaitForSeconds(1.25f);
        _openingTransition.SetActive(false);
    }

    public void TurnCrabButtonsOff()
    {
        for (int i = 0; i < _crabContainer.childCount; i++)
        {
            Button CrabButton = _crabContainer.GetChild(i).GetChild(0).GetChild(0).GetComponent<Button>();
            CrabButton.interactable = false;
        }
    }

    public void TurnCrabButtonsOn()
    {
        for (int i = 0; i < _crabContainer.childCount; i++)
        {
            Button CrabButton = _crabContainer.GetChild(i).GetChild(0).GetChild(0).GetComponent<Button>();
            CrabButton.interactable = true;
        }
    }
}
