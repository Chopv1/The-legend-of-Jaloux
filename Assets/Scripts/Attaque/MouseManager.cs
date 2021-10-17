using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedObject;

    private Camera cam;
    private RaycastHit2D hitInfo;
    private LayerMask layerMask;
    void Start()
    {
        cam = Camera.main; //On garde la camera dans une variable
        
    }
    void Update()
    {
        SelectAnObject(); //fonction pour selectionner l'objet
    }
    private void SelectAnObject()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition); //Le rayon pour récupére l'info de quel object on a touché
            hitInfo = Physics2D.Raycast(rayCastPos, Vector2.zero); //On fait le rayon et on l'enrengistre dans une variable
            
            if (hitInfo.collider != null) //Si on a touché quelques chose c'est bon
            {
                GameObject hitObject = hitInfo.transform.gameObject;
                ObjectSelected(hitObject);
            }
            else
            {
                ClearSelection();
            }
        }
    }
    void ObjectSelected(GameObject hitObject)
    {
        if(selectedObject!=null)
        {
            if(hitObject==selectedObject)
            {
                return;
            }
            else
            {
                ClearSelection();
            }
        }

        GameObject hexagone = hitObject.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = true;
        selectedObject = hitObject;

        if(selectedObject == GameObject.Find("Player"))
        {
            hitObject.GetComponent<Player>().SetIsSelected();
        }
        if(selectedObject == GameObject.Find("Enemy"))
        { 
            hitObject.GetComponent<Enemy>().SetIsSelected();
        }
    }
    public void ClearSelection()
    {
        if(selectedObject!=null)
        {
            GameObject hexagone = selectedObject.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }
        selectedObject = null;
    }

}
