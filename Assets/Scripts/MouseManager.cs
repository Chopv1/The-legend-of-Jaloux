using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
  
    GameObject selectedObject;
    
    void Start()
    {
        
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Position de la souris
        RaycastHit hitInfo; // Variable pour stocker la position de la souris

        if( Physics.Raycast(ray, out hitInfo)) // Si y a un objet à la position de la souris et son LayerMask est le bon
        {
            GameObject hitObject = hitInfo.transform.root.gameObject;
            SelectObject(hitObject);
        }
        else
        {
            ClearSelection();
        }

        void SelectObject(GameObject obj)//Fonction pour selectionner l'objet 
        {
            if(selectedObject!=null)//si l'objet selectionner existe
            {
                if(obj==selectedObject)// et que l'objet selectionné est le même arrêter la fonction
                {
                    return; //Forcer la fin de la fonction vu que c'est void il return rien
                }
                else 
                {
                    ClearSelection(); //Vider la selection
                }
            }
            selectedObject = obj;//Selectionner l'objet si tout est bon
        }
        void ClearSelection() // Methode pour vider la selection
        {
            selectedObject = null;
        }
    }
}
