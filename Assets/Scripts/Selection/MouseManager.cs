using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedObject1;
    public GameObject selectedObject2;
    public GameObject mapPreFab;

    private Camera cam;
    private RaycastHit2D hitInfo;
    private GameObject hitObject;
    private bool playerSelected;
    private GameObject[] tableauTileGrass;
    private bool enemySelected;

    void Start()
    {
        enemySelected = false;
        playerSelected = false;
        cam = Camera.main; //On garde la camera dans une variable
    }
    void Update()
    {
        SelectAnObject(); //fonction pour selectionner l'objet
    }
    public void playerNotSelected()
    {
        playerSelected = false;
    }
    private void SelectAnObject()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition); //Le rayon pour r�cup�re l'info de quel object on a touch�
            hitInfo = Physics2D.Raycast(rayCastPos, Vector2.zero); //On fait le rayon et on l'enrengistre dans une variable
            
            if (hitInfo.collider != null ) //Si on a touch� quelques chose c'est bon
            {
                playerSelected = true;
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
            if(hitObject == selectedObject1)
            {
                return;
            }
            else if(selectedObject1.CompareTag("Unit") && !selectedObject1.GetComponent<Unit>().IsInReach(hitObject))
            {
                selectedObject2 = hitObject;
                selectedObject2.GetComponent<Enemy>().SetIsSelectedObject2(true);
            }
            else
            {
                ClearSelection();
            }
        }

        if (!hitObject.CompareTag("Unit") && !hitObject.GetComponent<Enemy>().GetAttacked())
        {
            selectedObject1 = hitObject;
            GameObject hexagone = hitObject.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            //hitObject.GetComponent<Enemy>().AfficherStats();
        }
        else if(selectedObject1 != null && !selectedObject1.CompareTag("Unit") && selectedObject2 != null && !hitObject.CompareTag("Unit") && hitObject.GetComponent<Enemy>().GetAttacked())
        {
            hitObject.GetComponent<Enemy>().ChangeAttacked(false);
        }
        else if(hitObject.CompareTag("Unit"))
        {
            selectedObject1 = hitObject;
            GameObject hexagone = hitObject.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            //hitObject.GetComponent<Player>().AfficherStats();
        }
        else
        {
            hitObject.GetComponent<Enemy>().ChangeAttacked(false);
        }

        

        if (selectedObject2==null)
        {
            if(selectedObject1!= null && selectedObject1.CompareTag("Unit"))
            {
                hitObject.GetComponent<Unit>().SetIsSelected(true);
                Debug.Log("Clic sur le Joueur");

                CanMove(true);
            }
            if(selectedObject1 != null && selectedObject1.CompareTag("Enemy"))
            { 
                hitObject.GetComponent<Enemy>().SetIsSelected(true);
                Debug.Log("Clic sur l'ennemy");
            }

        }
    }
    public void ClearSelection()
    {
        if(selectedObject1 != null)
        {
            if (selectedObject1.CompareTag("Unit"))
            {
                selectedObject1.GetComponent<Unit>().SetIsSelected(false);
            }
            if (selectedObject1.CompareTag("Enemy"))
            {
                selectedObject1.GetComponent<Enemy>().SetIsSelected(false);
            }

            GameObject hexagone = selectedObject1.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;

            /*GameObject fenetre = selectedObject1.transform.GetChild(1).gameObject;
            fenetre.GetComponent<SpriteRenderer>().enabled = false;
            GameObject stats = GameObject.Find("Stats");
            stats.GetComponent<Text>().enabled = false;*/

            if (selectedObject2!=null)
            {
                hexagone = selectedObject2.transform.GetChild(0).gameObject;
                hexagone.GetComponent<SpriteRenderer>().enabled = false;
                hexagone.GetComponent<SpriteRenderer>().color = Color.white;
                selectedObject2.GetComponent<Enemy>().SetIsSelectedObject2(false);
            }
        }
        selectedObject1 = null;
        selectedObject2 = null;
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(new Vector2(0,0), 50);
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public GameObject getSelection()
    {
        return selectedObject1;
    }
    public void CanMove(bool yesOrNo)
    {
        tableauTileGrass = GameObject.FindGameObjectsWithTag("TileGrass");
        foreach (GameObject tile909 in tableauTileGrass)
        {
            tile909.GetComponent<BoxCollider>().enabled = yesOrNo;
        }
    }

}
