using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Unit : MonoBehaviour
{

    //Variable du script player
    private Camera cam;
    public int currentPv;
    public int attack;
    public int defense;
    public float reach;
    public bool isSelected;
    public int pa;

    //Variable du script player
    public LayerMask enemyLayer;
    public int MaxPv = 100;
    public MouseManager mouse;

    //Variable du script initial
    public int tileX;
    public int tileY;
    public TileMap map;
    public GameObject player;
    public Transform movePoint;
    public ClickableTile target;
    public GameObject path;
    List<GameObject> pathList = new List<GameObject>();
    public bool launchMove = false;
    public int points = 5;
    public GameObject boutonAvancer;
    public GameObject boutonFinTour;
    public GameObject boutonFouille;
    public GameObject compteurPA;
    public GameObject mapPreFab;
    public GameObject mouseManagerObject;
    private GameObject[] tableauTileGrass;
    public List<int> listObjets;
    public List<Items> listItems;
    public List<Node> currentPath = null;

    void Start()
    {
        //Player's Script
        currentPv = MaxPv;
        cam = Camera.main;
        isSelected = false;
        attack = 70;
        reach = 1f;
        pa = 10;
        defense = 50;

        //Unit's script

        compteurPA = GameObject.Find("Compteur PA");

        compteurPA.GetComponent<Text>().enabled = true;
        

        boutonAvancer = GameObject.Find("Button Avance");

        boutonAvancer.GetComponent<Button>().interactable = false;


        boutonFinTour = GameObject.Find("Button FinTour");


        movePoint.GetComponent<Renderer>().enabled = false;

        boutonFouille = GameObject.Find("Button Fouille");


        boutonFouille.GetComponent<Button>().enabled = true;
    }

    void Update(){
        //Player's Script
        if (isSelected)
        {
            CanAttack();
        }
        //Unit's script
        if (currentPath != null){
            int currNode = 0;
            boutonAvancer.GetComponent<Button>().interactable = true;




            while (currNode < currentPath.Count - 1){
                //Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y);

                //Debug.DrawLine(start, end, Color.red);

             
                    GameObject go = (GameObject)Instantiate(path, end, Quaternion.identity);
                    pathList.Add(go);

                    currNode++;
                    
                
                
            }
            foreach (GameObject go in pathList)
            {
                Destroy(go, 0.1f);

            }
            pathList.Clear();

            
           
        }

        if(currentPath != null && Vector3.Distance(player.transform.position, movePoint.position) == 0 && launchMove == true){
            boutonFinTour.GetComponent<Button>().interactable = false;
            movePoint.GetComponent<Renderer>().enabled = true;
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            

            if (currentPath.Count == 1){
                map.pa = map.pa - map.i + 1;
                currentPath = null;
                launchMove = false;
                //target.GetComponent<Renderer>().material.color = new Color(0.5849056f, 0.5403813f, 0.4773051f, 1);
                movePoint.GetComponent<Renderer>().enabled = false;
                boutonFinTour.GetComponent<Button>().interactable = true;
                boutonAvancer.GetComponent<Button>().interactable = false;

                tableauTileGrass = GameObject.FindGameObjectsWithTag("TileGrass");
                foreach(GameObject tile909 in tableauTileGrass)
                {
                    tile909.GetComponent<BoxCollider>().enabled=false;
                }
                mouseManagerObject.GetComponent<MouseManager>().playerNotSelected();


            }
        } 
        compteurPA.GetComponent<Text>().text = "PA : " + map.pa.ToString();
    }


    public void Move(){
        if ( map.action==true) //v�rification nombre de pa
        {
  
            launchMove = true;
            boutonFouille.GetComponent<Button>().interactable = true;


        }


        //mettre la v�rification de la distance dans une fonction de au clic sur la case et non le bouton
        //permettre donc d'interdire cette fonction de clic sur une case lorsque launchMove est true
    }

    //Fusion des deux scripts pour avoir un seul scripts 
    //Player's Script
    public void SetIsSelected(bool selected) ///Fonction pour sélectionner le perso ou pas
    {
        isSelected = selected;

    }
    public void CanAttack() ///On regard s'il peut attaquer ou pas
    {

        ///La fonction trace un cercle de rayon 'reach' et enregistre tous les enemies dans ce cercle 
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer); 

        ChangeHexagoneColorToBlack(hitInfo);  //On affiche l'hexagone noir pour montrer qu'ils sont attackable

        Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition); // On réupère la position de la souris au moment du clic
        RaycastHit2D obj = Physics2D.Raycast(rayCastPos, Vector2.zero, enemyLayer); //On stock l'objet à l'endroit de la souris

        //Vérification qu'il peut attacker l'ennemie choisi
        if (Input.GetMouseButtonDown(0) && isSelected && obj.collider != null && obj.transform.gameObject.CompareTag("Enemy") && IsInReach(obj.transform.gameObject))
        {
            Debug.Log("Attacking");
            SetIsSelected(false); // On déselectionne
            mouse.GetComponent<MouseManager>().ClearSelection(); // On déselctionne
            mouse.GetComponent<MouseManager>().CanMove(false); // Plus de déplacement faut reselectionner le joueur pour se redéplacer après l'attaque
            obj.transform.gameObject.GetComponent<Enemy>().IsAttacked(attack); //On change le statut de l'ennemy en attaqué
            pa -= 1;
            ChangeHexagoneColorToWhite(hitInfo); //On remet les hexagones blanc et on les déaffiche
            
            
        }

        if (Input.GetMouseButtonDown(0) && obj.collider == null)// Si on touche rien
        {
            ChangeHexagoneColorToWhite(hitInfo);//On remet les hexagones blanc et on les déaffiche
            mouse.GetComponent<MouseManager>().ClearSelection(); //On clear les séléctions
        }
        SetIsSelected(false);
    }

    //On vérifie que l'objet en paramètre est dans la portée
    public bool IsInReach(GameObject obj)
    {
        bool reachable = false;
        ///La fonction trace un cercle de rayon 'reach' et enregistre tous les enemies dans ce cercle 
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
        foreach (Collider2D hit in hitInfo)
        {   
            //on vérifie si l'objet en paramètre est bien dans le cercle on renvoie true si il l'est
            if (hit.gameObject == obj)
            {
                reachable = true;
            }
        }
        return reachable;
    }
    public bool GetSelected() //Un getter
    {
        return this.isSelected;
    }

    private void ChangeHexagoneColorToWhite(Collider2D[] hitInfo)
    {   
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void ChangeHexagoneColorToBlack(Collider2D[] hitInfo)
    {
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            hexagone.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    public void AfficherStats()
    {
        GameObject fenetre = this.transform.GetChild(1).gameObject;
        fenetre.GetComponent<SpriteRenderer>().enabled = true;
        GameObject stats = GameObject.Find("Stats");
        stats.GetComponent<Text>().enabled = true;
        stats.GetComponent<Text>().text = "Stats\n----------------\nPV : " + currentPv + "/" + MaxPv + "\nAttaque : " + attack + "\nD�fense : " + defense + "\nPA : " + pa;
    }

    public void Fouille()
    {
        if (map.pa > 0)
        {
            boutonFouille.GetComponent<Button>().interactable = false;
            Random rand = new Random();
            int number = rand.Next(11);
            switch (number)
            {
                case 0:
                    listItems.Add(new Items("Casque en bronze", "Casque", 2));
                    break;
                case 1:
                    listItems.Add(new Items("Bottes de bronze", "Bottes", 2));
                    break;
                case 2:
                    listItems.Add(new Items("Potion", "Soin", 10));
                    break;
                case 3:
                    listItems.Add(new Items("Armure en bronze", "Armure", 2));
                    break;
                case 4:
                    listItems.Add(new Items("Armure de Jaloux", "Armure", 5));
                    break;
                case 5:
                    listItems.Add(new Items("Epée Divive", "Arme", 5));
                    break;
                case 6:
                    listItems.Add(new Items("Epée de bronze", "Arme", 2));
                    break;
                case 7:
                    listItems.Add(new Items("Casque en or", "Casque", 5));
                    break;
                case 8:
                    listItems.Add(new Items("Bottes en or", "Bottes", 5));
                    break;
                case 9:
                    listItems.Add(new Items("Jambière en bronze", "Jambières", 2));
                    break;
                case 10:
                    listItems.Add(new Items("Jambière en or", "Jambières", 5));
                    break;
            }
            map.pa = map.pa - 1;
        }
    }
}
