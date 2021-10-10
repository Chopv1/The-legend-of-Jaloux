using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* import de sceneManagemnt pour changer de scene */
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string nomScene; // pour une recherche de scene avec le nom
    public int indiceScene; // pour une recherche de scene avec l'indice
    void Start()
    {
        
    }

    // Update is called once per frame
    void ChangementScene()
    {
        SceneManager.LoadScene(1); // argument avec int ds build Settings  ou str = nom de la scene
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangementScene();
    }
}
