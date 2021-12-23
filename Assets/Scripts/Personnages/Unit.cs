using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    //Variable du script player
    private Camera cam;
    private int currentPv;
    private int attack;
    private int defense;
    private float reach;
    private bool isSelected;
    private int pa;

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
    public GameObject compteurPA;
    public GameObject mapPreFab;
    public GameObject mouseManagerObject;
    private GameObject[] tableauTileGrass;




    public List<Node> currentPath = null;

    public int CurrentPv { get => currentPv; set => currentPv = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public float Reach { get => reach; set => reach = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public int Pa { get => pa; set => pa = value; }

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
        Debug.Log(compteurPA);
        compteurPA.transform.position = this.transform.position + new Vector3(180f, 280f, 0);

        compteurPA.GetComponent<Text>().enabled = true;
        

        boutonAvancer = GameObject.Find("Button Avance");
        Debug.Log(boutonAvancer);
        boutonAvancer.GetComponent<Button>().transform.position = this.transform.position + new Vector3(180f,207.3f,0);
        boutonAvancer.GetComponent<Button>().interactable = false;


        boutonFinTour = GameObject.Find("Button FinTour");
        Debug.Log(boutonFinTour);
        boutonFinTour.GetComponent<Button>().transform.position = this.transform.position + new Vector3(180f, 240f, 0);

        movePoint.GetComponent<Renderer>().enabled = false;
    }

    void Update(){
        //Player's Script
        if (isSelected)
        {
            ShowAttack();
        }
        //Unit's script
        print(map.pa);
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
            Debug.Log(pathList.Count);

            
           
        }

        if(currentPath != null && Vector3.Distance(player.transform.position, movePoint.position) == 0 && launchMove == true)
        {
            boutonFinTour.GetComponent<Button>().interactable = false;
            movePoint.GetComponent<Renderer>().enabled = true;
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            

            if (currentPath.Count == 1)
            {
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


            }
        } 
        compteurPA.GetComponent<Text>().text = "PA : " + map.pa.ToString();
    }


    public void Move()
    {
        if ( map.action==true) //v�rification nombre de pa
        {
  
            launchMove = true;
  

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
    public bool ShowAttack() ///On regard s'il peut attaquer ou pas
    {

        ///La fonction trace un cercle de rayon 'reach' et enregistre tous les enemies dans ce cercle 
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer); 

        ChangeHexagoneColorToBlack(hitInfo);  //On affiche l'hexagone noir pour montrer qu'ils sont attackable
        
        return false;
    }
    public bool CanAttack(GameObject enemy)
    {
        Debug.Log(enemy);
        //Vérification qu'il peut attacker l'ennemie choisi
        if (IsInReach(enemy))
        {
            return true;
        }
        return false;
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
}
