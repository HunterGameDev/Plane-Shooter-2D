using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        Debug.Log("Options was selected.");
    }

    public void OpenCredits()
    {
        Debug.Log("Credits was selected.");
    }

    public void ExitGame()
    {
        Debug.Log("Quit was selected.");
    }
}
