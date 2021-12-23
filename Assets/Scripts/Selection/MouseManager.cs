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
            
            if (hitInfo.collider != null ) //Si on a touche quelques chose c'est bon
            {
                playerSelected = true;
                hitObject = hitInfo.transform.gameObject; // On stock les info de l'objet toucher dans une variable
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
            if (hitObject == selectedObject1) //Si l'objet touché est le même que le sélectionner on arrête la fonction
            {
                return;
            }
            else if (selectedObject1.CompareTag("Unit") && !selectedObject1.GetComponent<Unit>().IsInReach(hitObject)) //si on clic sur un enemy pas dans la portée du joueur après l'avoir sélectionné
            {
                selectedObject2 = hitObject; //on stock l'ennemy dans une vriable
                selectedObject2.GetComponent<Enemy>().SetIsSelectedObject2(true);
                Debug.Log("Enemy2");
            }
            else if (hitObject.CompareTag("Enemy") && selectedObject2==null && (selectedObject1.CompareTag("Unit")&&!selectedObject1.GetComponent<Unit>().IsInReach(hitObject))) //Des vérifications pour clear si on touche personne
            {
                ClearSelection();
            }
        }

        //Vérification qu'on selection un ennemy 
        if ((selectedObject1==null || selectedObject1.CompareTag("Enemy")) && hitObject.CompareTag("Enemy") && hitObject!=selectedObject1 && !hitObject.GetComponent<Enemy>().GetAttacked())
        {
            selectedObject1 = hitObject;
            GameObject hexagone = hitObject.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            hitObject.GetComponent<Enemy>().AfficherStats();
        }
        else if (hitObject.CompareTag("Unit")) //Si c'est un héro on clear la sélection pour le sélectionner
        {
            ClearSelection();
            selectedObject1 = hitObject;
            GameObject hexagone = hitObject.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            //hitObject.GetComponent<Unit>().AfficherStats();
            Debug.Log("Unit");


        }
        else if(hitObject.CompareTag("Enemy")) //Si c'est un ennemy qu'on a touché on change son statut d'attaqué (lors de l'attaque)
        {
            hitObject.GetComponent<Enemy>().ChangeAttacked(false);
        }

        

        if (selectedObject2==null)
        {
            //Verif pour selectionner le héro
            if(selectedObject1!= null && selectedObject1.CompareTag("Unit"))
            {
                selectedObject1.GetComponent<Unit>().SetIsSelected(true);
                Debug.Log("Clic sur le Joueur");

                CanMove(true);
            }
            //Verif pour selectionner l'ennemy
            if(selectedObject1 != null && selectedObject1.CompareTag("Enemy"))
            {
                selectedObject1.GetComponent<Enemy>().SetIsSelected(true);
                Debug.Log("Clic sur l'ennemy");
            }

        }
    }

    public void ClearSelection()
    {
        if (selectedObject1 != null) //Pour éviter les bugs
        {
            //Verif pour déselectionner dans le bon script
            if (selectedObject1.CompareTag("Unit"))
            {
                selectedObject1.GetComponent<Unit>().SetIsSelected(false);
            }
            if (selectedObject1.CompareTag("Enemy"))
            {
                selectedObject1.GetComponent<Enemy>().SetIsSelected(false);
                selectedObject1.GetComponent<Enemy>().EnleverStats();
            }
            //Adios les hexagone
            GameObject hexagone = selectedObject1.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;

            /*GameObject fenetre = selectedObject1.transform.GetChild(1).gameObject;
            fenetre.GetComponent<SpriteRenderer>().enabled = false;
            GameObject stats = GameObject.Find("Stats");
            stats.GetComponent<Text>().enabled = false;*/

            //Verif pour déselectionner le deuxième
            if (selectedObject2 != null)
            {
                hexagone = selectedObject2.transform.GetChild(0).gameObject;
                hexagone.GetComponent<SpriteRenderer>().enabled = false;
                hexagone.GetComponent<SpriteRenderer>().color = Color.white;
                selectedObject2.GetComponent<Enemy>().SetIsSelectedObject2(false);
            }
        }
        //tout a null le but du déselection
            selectedObject1 = null;
            selectedObject2 = null;
            Collider2D[] hitInfo = Physics2D.OverlapCircleAll(new Vector2(0, 0), 50); // Pour être sur on trace le plus grand cercle et on leur enlève l'hexagone
            foreach (Collider2D hit in hitInfo)
            {
                GameObject hexagone = hit.transform.GetChild(0).gameObject;
                hexagone.GetComponent<SpriteRenderer>().enabled = false;
                hexagone.GetComponent<SpriteRenderer>().color = Color.white;
            }

    }
    public GameObject getSelection()
    {
        return selectedObject1; //Simple getter
    }
    public void CanMove(bool yesOrNo) //Pour activer les déplacement
    {
        tableauTileGrass = GameObject.FindGameObjectsWithTag("TileGrass");
        foreach (GameObject tile909 in tableauTileGrass)
        {
            tile909.GetComponent<BoxCollider>().enabled = yesOrNo;
        }
    }

}