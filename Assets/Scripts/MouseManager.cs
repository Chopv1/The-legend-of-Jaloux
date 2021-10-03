using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedObject;
    private Camera cam;
    private RaycastHit2D hitInfo;
    public Player player;
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
            RaycastHit2D hitInfo = Physics2D.Raycast(rayCastPos, Vector2.zero); //On fait le rayon et on l'enrengistre dans une variable
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
        Debug.Log("Vous pouvez attaquer "+hitObject);
        Attack();


        selectedObject = hitObject;


    }
    void ClearSelection()
    {
        if(selectedObject!=null)
        {
            GameObject hexagone = selectedObject.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
        }
        
        selectedObject = null;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {

            player.Infliger();
            
        }
    }

}
