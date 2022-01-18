using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{    
    public string leveltoload;
    public GameObject DiffWindow;
    public AudioSource SongBouton;

    public void StartGame() 
    {
        SceneManager.LoadScene(leveltoload);
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
    public void JouerSongBouton()
    {
        SongBouton.Play();
    }

}

