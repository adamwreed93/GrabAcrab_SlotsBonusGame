using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _openingTransition;
    [SerializeField] private GameObject _endingTransition;
    [SerializeField] private Animator _girlAnimator;

    void Start()
    {
        StartCoroutine(DisableOpeningTransitionAnimation());
    }

    private IEnumerator DisableOpeningTransitionAnimation()
    {
        yield return new WaitForSeconds(1.04f);
        _openingTransition.SetActive(false);
    }


    public void SequenceSelectedButton()
    {   
        StartCoroutine(EnableEndingTransitionAnimation());
    }

    private IEnumerator EnableEndingTransitionAnimation()
    {
        _girlAnimator.SetBool("BlowAKiss", true);
        yield return new WaitForSeconds(1.5f);
        _endingTransition.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene(1);
    }
}
