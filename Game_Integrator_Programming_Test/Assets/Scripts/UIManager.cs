using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour
{
    GameManager _gameManager;

    [SerializeField] private GameObject _openingTransition;
    [SerializeField] private GameObject _endingTransition;
    [SerializeField] private GameObject _countdownTextObject;
    [SerializeField] private Transform _crabContainer;
    [SerializeField] private Text _countdownText;
    [SerializeField] private Text _totalWinningsSignText;
    [SerializeField] private Text _youWonSignWinningsText;
    [SerializeField] private int _countdownCurrentTime = 20;

    private int _numberOfCrabsPicked = -1;
    private int _crabNumberChecker = -1;
    private float _totalWinningsAmmount;

    

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(DisableTransitionAnimation());
        StartCoroutine(PickACrabCountdown());

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL!");
        }
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
        _totalWinningsAmmount += _gameManager.GetActiveSequence.Sequence[_numberOfCrabsPicked];
        _totalWinningsSignText.text = "$" + _totalWinningsAmmount + ".00";
        _youWonSignWinningsText.text = "$" + _totalWinningsAmmount + ".00";
    }

    private IEnumerator PickACrabCountdown()
    {
        while (_numberOfCrabsPicked != _gameManager.GetActiveSequence.Sequence.Length - 1)
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
            Button CrabButton = _crabContainer.GetChild(i).GetChild(1).GetChild(0).GetComponent<Button>();
            CrabButton.interactable = false;
        }
    }

    public void TurnCrabButtonsOn()
    {
        for (int i = 0; i < _crabContainer.childCount; i++)
        {
            Button CrabButton = _crabContainer.GetChild(i).GetChild(1).GetChild(0).GetComponent<Button>();
            CrabButton.interactable = true;
        }
    }
}
