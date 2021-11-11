using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvirParametres : MonoBehaviour
{
    public GameObject settingsWindow;
    public int estOuvert = 0;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscParametres();
        }

    }

    void Start()
    {
        settingsWindow.SetActive(false); 
    }
    
    public void EscParametres()
    {
        if(estOuvert == 0)
        {
            settingsWindow.SetActive(true);
            estOuvert = 1;
        }
        else
        {
            settingsWindow.SetActive(false);
            estOuvert = 0;
        }


    }
    public void QuitterParametres () 
    {
        settingsWindow.SetActive(false);
        estOuvert = 0;
    } 
}
