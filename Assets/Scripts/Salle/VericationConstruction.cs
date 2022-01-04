using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VericationConstruction  : MonoBehaviour
{
    // Start is called before the first frame update
    public int nombreOuverture;
    public int porteH ;
    public int porteD;
    public int porteB;
    public int porteG;
    public int[] portes;
    public GameObject[] salles;
    void Start()
    {
        portes = new int[] { this.porteH, this.porteD, this.porteB, this.porteG } ;
        foreach(int porte in portes)
        {
            if (porte == 1)
            {
                nombreOuverture += 1;
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("SpawnPoint"))
        {

        }
    }

}
