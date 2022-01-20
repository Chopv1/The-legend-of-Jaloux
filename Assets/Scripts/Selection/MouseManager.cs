using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    private GameObject selectedObject1;
    private GameObject selectedObject2;
    public GameObject mapPreFab;
    public GameObject unit;
    public LayerMask enemyLayer;
    private int sort = 0;
    private Camera cam;
    private RaycastHit2D hitInfo;
    private GameObject hitObject;
    private GameObject[] tableauTileGrass;
    private GameObject bExit;
    private GameObject bInfo;
    private GameObject bAttaque;
    public Animator herosAnimator;
    private bool sortSelected = false;

    void Start()
    {
        cam = Camera.main; //On garde la camera dans une variable

        bExit = GameObject.Find("Ne rien faire");
        bAttaque = GameObject.Find("Attaquer");
        bAttaque.GetComponent<Button>().interactable = false;
    }
    void Update()
    {
        SelectAnObject(); //fonction pour selectionner l'objet
        ShowUi();
    }
    
    private void SelectAnObject()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition); //Le rayon pour r�cup�re l'info de quel object on a touch�
            hitInfo = Physics2D.Raycast(rayCastPos, Vector2.zero); //On fait le rayon et on l'enrengistre dans une variable
            
            if (hitInfo.collider != null ) //Si on a touche quelques chose c'est bon
            {
                hitObject = hitInfo.transform.gameObject; // On stock les info de l'objet toucher dans une variable
                ObjectSelected(hitObject);
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
            else if(selectedObject1.CompareTag("Unit") && hitObject.CompareTag("Enemy") &&selectedObject1.GetComponent<Unit>().IsInReach(hitObject))
            {
                selectedObject2 = hitObject;
                selectedObject2.GetComponent<Enemy>().ChangeHexagoneColorToBleu(selectedObject2);
            }
            else if (hitObject.CompareTag("Enemy") && selectedObject2==null && (selectedObject1.CompareTag("Unit")&&!selectedObject1.GetComponent<Unit>().IsInReach(hitObject))) //Des vérifications pour clear si on touche personne
            {
                ClearSelection();

            }
        }

        //Vérification qu'on selection un ennemy 
        if ((selectedObject1==null || selectedObject1.CompareTag("Enemy")) && hitObject.CompareTag("Enemy") && hitObject!=selectedObject1)
        {
            ClearSelection();
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


        }
        

        

        if (selectedObject2==null)
        {
            //Verif pour selectionner le héro
            if(selectedObject1!= null && selectedObject1.CompareTag("Unit"))
            {
                selectedObject1.GetComponent<Unit>().SetIsSelected(true);

                CanMove(true);
            }
            //Verif pour selectionner l'ennemy
            if(selectedObject1 != null && selectedObject1.CompareTag("Enemy"))
            {
                selectedObject1.GetComponent<Enemy>().SetIsSelected(true);

                CanMove(false);
            }

        }
    }
    public void boutonStop(){
        herosAnimator.SetBool("isCircleAttacking", false);
        herosAnimator.SetBool("isDigging", false);
        herosAnimator.SetBool("isMoving", false);
        herosAnimator.SetBool("isRightAttacking", false);
        ClearSelection();
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


            //Verif pour déselectionner le deuxième
            if (selectedObject2 != null)
            {
                hexagone = selectedObject2.transform.GetChild(0).gameObject;
                hexagone.GetComponent<SpriteRenderer>().enabled = false;
                hexagone.GetComponent<SpriteRenderer>().color = Color.white;
                selectedObject2.GetComponent<Enemy>().SetIsSelected(false);
            }
        }
        //tout a null le but du déselection
        selectedObject2 = null;
        selectedObject1 = null;
        sortSelected = false;
        /*Collider2D[] hitInfo = Physics2D.OverlapCircleAll(new Vector2(0, 0), 50); // Pour être sur on trace le plus grand cercle et on leur enlève l'hexagone
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }*/
    }
    public GameObject getSelection()
    {
        return selectedObject1; //Simple getter
    }
    public GameObject getSelection2()
    {
        return selectedObject2; //Simple getter
    }
    public void CanMove(bool yesOrNo) //Pour activer les déplacement
    {
        tableauTileGrass = GameObject.FindGameObjectsWithTag("TileGrass");
        foreach (GameObject tile909 in tableauTileGrass)
        {
            tile909.GetComponent<BoxCollider>().enabled = yesOrNo;
        }
    }
    public void ShowUi()
    {
        if (selectedObject1 != null)
        {
            if (sort!=2 && selectedObject2 != null && selectedObject2.CompareTag("Enemy") && selectedObject1.CompareTag("Unit") && selectedObject1.GetComponent<Unit>().IsInReach(selectedObject2) && sortSelected)
            {
                bAttaque.GetComponent<Button>().interactable = true;
            }
            else if(sort==2 && selectedObject1.CompareTag("Unit"))
            {
                bAttaque.GetComponent<Button>().interactable = true;
            }
            else
            {
                bAttaque.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void AfficherStats()
    {
        
    }
    public void attack()
    {
        if (sort!=2 && selectedObject1.CompareTag("Unit") && selectedObject2.CompareTag("Enemy") && selectedObject1.GetComponent<Unit>().CanAttack(selectedObject2))
        {
            selectedObject2.GetComponent<Enemy>().IsAttacked(selectedObject1.GetComponent<Unit>().Attack);
            bAttaque.GetComponent<Button>().interactable = false;

            herosAnimator.SetBool("isCircleAttacking", false);
            herosAnimator.SetBool("isDigging", false);
            herosAnimator.SetBool("isMoving", false);
            herosAnimator.SetBool("isRightAttacking", false);
            herosAnimator.SetBool("isLeftAttacking", false);
            herosAnimator.SetBool("isDownAttacking", false);
            herosAnimator.SetBool("isUpAttacking", false);


            if (selectedObject1.transform.position.x > selectedObject2.transform.position.x)
                        {
                            herosAnimator.SetBool("isLeftAttacking", true);

                        } else if (selectedObject1.transform.position.x < selectedObject2.transform.position.x)
                        {
                            herosAnimator.SetBool("isRightAttacking", true);
                        } else if (selectedObject1.transform.position.y < selectedObject2.transform.position.y)
                        {
                            herosAnimator.SetBool("isUpAttacking", true);
                        } else
                        {
                            herosAnimator.SetBool("isDownAttacking", true);
                        }

                        
            // MovementEnnemy
            /* if (selectedObject2.name == "Enemy")
             {


                 ennemi1Animator.SetBool("isAttacked", true);

             }
             else if (selectedObject2.name == "Enemy2")
             {

                 ennemi2Animator.SetBool("isAttacked", true);
             }*/


            ClearSelection();
            

        }
        else if(sort==2&&selectedObject1.CompareTag("Unit")&&selectedObject1.GetComponent<Unit>().EnemyAround())

        {

            herosAnimator.SetBool("isDigging", false);
            herosAnimator.SetBool("isMoving", false);
            herosAnimator.SetBool("isRightAttacking", false);
            herosAnimator.SetBool("isLeftAttacking", false);
            herosAnimator.SetBool("isDownAttacking", false);
            herosAnimator.SetBool("isUpAttacking", false);
            herosAnimator.SetBool("isCircleAttacking", true);
            selectedObject1.GetComponent<Unit>().HitAllEnemy();
            bAttaque.GetComponent<Button>().interactable = false;
            ClearSelection();
        }
    }
    public void Sort1()
    {
        GameObject hero= GameObject.Find("Unit");
        HecagoneWhite();
        sort = 1;
        hero.GetComponent<Unit>().Sort(sort);
        sortSelected = true;
    }
    public void Sort2()
    {
        GameObject hero = GameObject.Find("Unit");
        sort = 2;
        HecagoneWhite();
        hero.GetComponent<Unit>().Sort(sort);
        sortSelected = true;

    }
    public void Sort3()
    {
        GameObject hero = GameObject.Find("Unit");
        sort = 3;
        HecagoneWhite();
        hero.GetComponent<Unit>().Sort(sort);
        sortSelected = true;
    }
    public void HecagoneWhite()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(new Vector2(0, 0), 50,enemyLayer); // Pour être sur on trace le plus grand cercle et on leur enlève l'hexagone
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }


    
}
