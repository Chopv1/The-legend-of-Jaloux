using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetsInventaire : MonoBehaviour
{
    public Unit HeroGuerrier;
    private int indice = 0;
    private string NomObjet;
    // Update is called once per frame

    private GameObject CaseInventaire;

    void Update()
    {
        placageItems();
    }

    public void placageItems()
    {
        if(HeroGuerrier.listItems.Count != 0)
        {
            Debug.Log("Liste pas nul");
            indice = 0;

            foreach (Items o in HeroGuerrier.listItems)
            {
                if(indice == 0)
                { 
                    NomObjet = "ObjetInventaire"; 
                }
                else 
                { 
                    NomObjet = "ObjetInventaire (" + indice + ")"; 
                
                }
                CaseInventaire = GameObject.Find(NomObjet);
                indice++;
                GameObject TexteObjet = CaseInventaire.transform.GetChild(0).gameObject;
                Debug.Log(TexteObjet);
                TexteObjet.GetComponent<UnityEngine.UI.Text>().text = o.getNomItem();


            }
        }
    }
}
