using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{    
    public string leveltoload;
    public GameObject settingsWindow;
    public GameObject DiffWindow;
    public void StartGame() 
    {
        SceneManager.LoadScene(leveltoload);
    }
    public void MenuParametres () 
    {
        settingsWindow.SetActive(true);
    } 
    public void QuitterParametres () 
    {
        settingsWindow.SetActive(false);
    } 
    public void MenuDiff () 
    {
        DiffWindow.SetActive(true);
    } 
    public void QuitterDiff () 
    {
        DiffWindow.SetActive(false);
    }

    public void QuitterPartie() 
    {
        Application.Quit();
    }


}

