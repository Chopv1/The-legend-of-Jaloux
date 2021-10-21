using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedObject1;
    public GameObject selectedObject2;

    private Camera cam;
    private RaycastHit2D hitInfo;
    private GameObject hitObject;

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
                hitObject = hitInfo.transform.gameObject;
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
        if(selectedObject1 != null)
        {
            if(hitObject== selectedObject1)
            {
                return;
            }
            else if(selectedObject1 == GameObject.Find("Player") && !selectedObject1.GetComponent<Player>().IsInReach(hitObject))
            {
                selectedObject2 = hitObject;
                selectedObject2.GetComponent<Enemy>().SetIsSelectedObject2();
            }
            else
            {
                ClearSelection();
            }
        }

        if (selectedObject1 != GameObject.Find("Player") && hitObject != GameObject.Find("Player"))
        {
            selectedObject1 = hitObject;
        }
        else if(hitObject==GameObject.Find("Player"))
        {
            selectedObject1 = hitObject;
        }

        GameObject hexagone = hitObject.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = true;
        
        if(selectedObject2==null)
        {
            if(selectedObject1 == GameObject.Find("Player"))
            {
                hitObject.GetComponent<Player>().SetIsSelected();
            }
            if(selectedObject1 == GameObject.Find("Enemy"))
            { 
                hitObject.GetComponent<Enemy>().SetIsSelected();
            }
        }
    }
    public void ClearSelection()
    {
        if(selectedObject1 != null)
        {
            if (selectedObject1 == GameObject.Find("Player"))
            {
                selectedObject1.GetComponent<Player>().SetIsSelected();
            }
            if (selectedObject1 == GameObject.Find("Enemy"))
            {
                selectedObject1.GetComponent<Enemy>().SetIsSelected();
            }
            GameObject hexagone = selectedObject1.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;

            if(selectedObject2!=null)
            {
                hexagone = selectedObject2.transform.GetChild(0).gameObject;
                hexagone.GetComponent<SpriteRenderer>().enabled = false;
                hexagone.GetComponent<SpriteRenderer>().color = Color.white;
            }
            
        }
        selectedObject1 = null;
        selectedObject2 = null;
    }
    public GameObject getSelection()
    {
        return selectedObject1;
    }

}
