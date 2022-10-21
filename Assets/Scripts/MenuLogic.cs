using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditsScreen;
    [SerializeField]
    private GameObject _optionsScreen;

    // Start is called before the first frame update
    void Start()
    {
        _creditsScreen.SetActive(false);
        _optionsScreen.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        _optionsScreen.SetActive(true);
    }

    public void OpenCredits()
    {
        _creditsScreen.SetActive(true);
    }

    public void GoBack()
    {
        _creditsScreen.SetActive(false);
        _optionsScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Quit was selected.");
        Application.Quit();
    }
}
