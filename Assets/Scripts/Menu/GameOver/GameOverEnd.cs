using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverEnd : MonoBehaviour
{
    public void LaLoose()
    {
        gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Deplacement2");
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
