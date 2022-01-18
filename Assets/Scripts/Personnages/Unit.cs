using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System;

public class Unit : MonoBehaviour
{
    public GameObject gameOverScreen;
    public ObjetsInventaire ObjetsDansInventaire;
    //Variable du script player
    private Camera cam;
    public int currentPv;
    public int attack;
    public int defense;
    public float reach;
    public bool isSelected;
    public int pa;
    public int coutPa;
    public int sort;

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
    public GameObject boutonAttaque;
    public GameObject compteurPA;
    public GameObject mapPreFab;
    public GameObject mouseManagerObject;
    private GameObject[] tableauTileGrass;
    public List<int> listObjets;
    public List<Items> listItems;
    public List<Node> currentPath = null;
    public Animator herosAnimator;
    public Animator ennemi1Animator;
    public Animator ennemi2Animator;

    public int CurrentPv { get => currentPv; set => currentPv = value; }
    public int Attack;
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
        attack = 20;
        reach = 1f;
        pa = 10;
        defense = 15;

        //Unit's script

        compteurPA = GameObject.Find("Compteur PA");

        compteurPA.GetComponent<Text>().enabled = true;
        

        boutonAvancer = GameObject.Find("Button Avance");

        boutonAvancer.GetComponent<Button>().interactable = false;


        boutonFinTour = GameObject.Find("Button FinTour");


        movePoint.GetComponent<Renderer>().enabled = false;

        boutonFouille = GameObject.Find("Button Fouille");


        boutonFouille.GetComponent<Button>().enabled = true;

        boutonAttaque = GameObject.Find("Attaquer");

        herosAnimator.SetBool("isRightAttacking", false);

    }

    void Update(){
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

            IsDead();
           
        }

        if(currentPath != null && Vector3.Distance(player.transform.position, movePoint.position) == 0 && launchMove == true)
        {
            boutonFinTour.GetComponent<Button>().interactable = false;
            movePoint.GetComponent<Renderer>().enabled = true;
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            herosAnimator.SetBool("isRightAttacking", false);
            herosAnimator.SetBool("isUpAttacking", false);
            herosAnimator.SetBool("isDownAttacking", false);
            herosAnimator.SetBool("isLeftAttacking", false);
            herosAnimator.SetBool("isCircleAttacking", false);
            ennemi1Animator.SetBool("isAttacked", false);
            ennemi2Animator.SetBool("isAttacked", false);
            herosAnimator.SetBool("isDigging", false);
            herosAnimator.SetBool("isMoving", true);

            if (currentPath.Count == 1)
            {
                map.pa = map.pa - map.i + 1;
                currentPath = null;
                launchMove = false;
                //target.GetComponent<Renderer>().material.color = new Color(0.5849056f, 0.5403813f, 0.4773051f, 1);
                movePoint.GetComponent<Renderer>().enabled = false;
                boutonFinTour.GetComponent<Button>().interactable = true;
                boutonAvancer.GetComponent<Button>().interactable = false;
                herosAnimator.SetBool("isMoving", false);

                tableauTileGrass = GameObject.FindGameObjectsWithTag("TileGrass");
                foreach(GameObject tile909 in tableauTileGrass)
                {
                    tile909.GetComponent<BoxCollider>().enabled=false;
                }


            }
        } 
        compteurPA.GetComponent<Text>().text = "PA : " + map.pa.ToString();
    }
    public float HPPercentage() 
    {
        return (currentPv);
    }

    public void Move()
    {
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
 
    public bool CanAttack(GameObject enemy)
    {
        //Vérification qu'il peut attacker l'ennemie choisi
        if (IsInReach(enemy)&&map.pa>=coutPa)
        {
            map.pa= map.pa-coutPa;

            return true;

        }
        return false;
    }
    public bool EnemyAround()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
        foreach (Collider2D hit in hitInfo)
        {
           if(hit!=null)
            {

                return true;
            }
        }
        return false;
    }
    public void HitAllEnemy()
    {
        if (map.pa >= coutPa)
        {
            Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);

            foreach(Collider2D hit in hitInfo)
            {
                ennemi2Animator.SetBool("isAttacked", true);
                ennemi1Animator.SetBool("isAttacked", true);
                hit.GetComponent<Enemy>().IsAttacked(this.Attack);
                Debug.Log(hit+" Touché");
            }

            map.pa = map.pa - coutPa;
            
        }

        ChangeHexagoneColorToWhite();
        
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
                ChangeHexagoneColorToBlackForOneEnemy(hit.gameObject);
            }
        }
        return reachable;
    }
    public bool GetSelected() //Un getter
    {
        return this.isSelected;
    }

    private void ChangeHexagoneColorToWhite()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void ChangeHexagoneColorToBlack()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            hexagone.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    private void ChangeHexagoneColorToBlackForOneEnemy(GameObject hit)
    {
        GameObject hexagone = hit.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = true;
        hexagone.GetComponent<SpriteRenderer>().color = Color.black;
    }
    private void ChangeHexagoneColorToBlue()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            hexagone.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    public void AfficherStats()
    {
        GameObject fenetre = this.transform.GetChild(1).gameObject;
        fenetre.GetComponent<SpriteRenderer>().enabled = true;
        GameObject stats = GameObject.Find("StatsHéros");
        stats.GetComponent<Text>().enabled = true;
        stats.GetComponent<Text>().text = "Stats\n----------------\nPV : " + currentPv + "/" + MaxPv + "\nAttaque : " + attack + "\nD�fense : " + defense + "\nPA : " + pa;
    }

    public void Fouille()
    {
        if (map.pa > 0)

        {
            herosAnimator.SetBool("isRightAttacking", false);
            herosAnimator.SetBool("isLeftAttacking", false);
            herosAnimator.SetBool("isDownAttacking", false);
            herosAnimator.SetBool("isUpAttacking", false);
            herosAnimator.SetBool("isCircleAttacking", false);
            ennemi1Animator.SetBool("isAttacked", false);
            ennemi2Animator.SetBool("isAttacked", false);
            herosAnimator.SetBool("isDigging", false);
            herosAnimator.SetBool("isDigging", true);


            boutonFouille.GetComponent<Button>().interactable = false;
            Random rand = new Random();
            int number = rand.Next(12);
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
                case 11:
                    listItems.Add(new Items("Grosse Potion", "Soin", 50));
                    break;
            }
            map.pa = map.pa - 1;
        }
    }
    public void Sort(int sort)
    {
        switch (sort)
        {
            case 1:
                coutPa = 2;
                if (map.pa >= coutPa)
                {
                    Attack = Convert.ToInt32(ObjetsDansInventaire.GetAttaqueHero() * 1.2);
                    print(Attack);
                    reach = 1f;
                    this.sort = 1;
                    ChangeHexagoneColorToBlue();
                }
                else
                {
                    boutonAttaque.GetComponent<Button>().interactable = false;
                    print("Vous n'avez pas assez de PA pour utiliser cette compétence");
                }
                break;
            case 2:
                coutPa = 7;
                if (map.pa >= coutPa)
                {
                    Attack = Convert.ToInt32(ObjetsDansInventaire.GetAttaqueHero() * 1);
                    print(Attack);
                    reach = 1f;
                    this.sort = 2;
                    ChangeHexagoneColorToBlack();
                }
                else
                {
                    boutonAttaque.GetComponent<Button>().interactable = false;
                    print("Vous n'avez pas assez de PA pour utiliser cette compétence");
                }
                break;
            case 3:
                coutPa = 5;
                if (map.pa >= coutPa)
                {
                    Attack = Convert.ToInt32(ObjetsDansInventaire.GetAttaqueHero() * 3);
                    print(Attack);
                    reach = 2f;
                    this.sort = 3;
                    ChangeHexagoneColorToBlue();
                }
                else
                {
                    boutonAttaque.GetComponent<Button>().interactable = false;
                    print("Vous n'avez pas assez de PA pour utiliser cette compétence");
                }
                break;
        }
    }

    public void isAttacked(int damage)
    {
        if(currentPv>0)
        {
            this.currentPv -= (damage - defense);
            Debug.Log("Unit Attacked");
            IsDead();
            isSelected = false;
        }
    }
    
    public void IsDead()
    {
        if(currentPv<=0)
        {
            currentPv = 0;
            GameOver();
        }
    }
    public void GameOver()
    {
        gameOverScreen.GetComponent<GameOverEnd>().LaLoose();
    }
}

