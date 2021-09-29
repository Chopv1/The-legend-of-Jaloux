using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject selection;
    Camera cam;
    RaycastHit2D hitInfo;
    bool selected=false;
    bool create = false;
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
            if(hitInfo.collider!=null) //Si on a touché quelques chose c'est bon
            {
               GameObject obj = hitInfo.collider.gameObject;
                switch(obj.tag)
               {
                    case "Joueur": 
                        selected = true;
                        create = true;
                        PlayersAction(obj);
                        break;
                    case "Ennemy":
                        EnnemysAction(obj);
                        break;
                    default:
                        selected = false;
                        break;
               }
            }
        }
    }
    public void PlayersAction(GameObject obj)
    {
        if(create)
        {
            selection.transform.position = obj.transform.position;
            selection.GetComponent<SpriteRenderer>().enabled = true;
            create = false;
        }
        while(Input.GetMouseButtonDown(0))
        {
            selection.GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
    public void EnnemysAction(GameObject obj)
    {
        if(create)
        {
            selection.GetComponent<SpriteRenderer>().enabled=true;
            selection.transform.position = obj.transform.position;
            create = false;
        }
        while(Input.GetMouseButtonDown(0))
        {
            selection.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
   
}
