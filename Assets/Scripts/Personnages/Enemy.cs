using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public GameObject selection;
    public int MaxPv = 100;



    public int currentPv;
    private int attack = 20;
    private int defense = 10;
    private float reach = 1f;
    private bool isSelected;
    private bool attacked = false;
    private int pa = 5;
    private bool canAttack;
    public LayerMask heroLayer;
    public GameObject fenetre;
    public GameObject stats;



    //Variiables necessaires au mouvement
    public int tileX;
    public int tileY;
    public TileMap map;
    public GameObject ennemy;
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


    // Start is called before the first frame update

    void Start()
    {
        canAttack = false;

        this.currentPv = MaxPv;
        isSelected = false;
        fenetre = GameObject.Find("CarréStats");
        stats = GameObject.Find("Stats");
        

        boutonAvancer = GameObject.Find("Button Avance");
    }

    // Update is called once per frame
    public void Update()
    {
        if(currentPath != null && currentPath.Count > 1 && Vector3.Distance(ennemy.transform.position, movePoint.position) == 0 && launchMove == true && pa > 0){
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            pa = pa - 1;
            

            if (currentPath.Count == 1){
                Debug.Log("Fin des haricots");
                CanAttack();
                pa = pa - map.j + 1;
                currentPath = null;
                launchMove = false;
                
                //target.GetComponent<Renderer>().material.color = new Color(0.5849056f, 0.5403813f, 0.4773051f, 1);

            }
        } 
        else if (pa < 1) {
            launchMove = false;
            pa = 5;
        }

    }


    public void Move(){
        if (currentPath != null && currentPath.Count > 1) //v�rification nombre de pa
        {
            Debug.Log(pa);
            launchMove = true;
  
        }
      

        //mettre la v�rification de la distance dans une fonction de au clic sur la case et non le bouton
        //permettre donc d'interdire cette fonction de clic sur une case lorsque launchMove est true
    }
    public void CanAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, reach, heroLayer);

        if(hit==null)
        {
            canAttack = false;
        }
        else if(hit.gameObject.CompareTag("Unit"))
        {
            canAttack = true;
            LunchAttack(hit);
        }
        else
        {
            canAttack = false;
        }
    }
    public void LunchAttack(Collider2D hit)
    {
        GameObject unit = hit.gameObject;

        unit.GetComponent<Unit>().isAttacked(Attack);
    }
    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
        if (!isSelected)
        {
            ChangeHexagoneColorToWhite(this.gameObject);
        }
    }
    private void IsDead()
    {
        if (currentPv <= 0)
        {
            this.currentPv = 0;
            this.gameObject.SetActive(false);
            ChangeHexagoneColorToWhite(this.gameObject);
        }
    }
    public void IsAttacked(int damage)
    {
        if (currentPv > 0)
        {
            this.currentPv -= (damage-defense);
            Debug.Log("Enemy Attacked");
            IsDead();
            ChangeHexagoneColorToWhite(this.gameObject);
            isSelected = false;
        }

    }

    public void ChangeHexagoneColorToBleu(GameObject obj)
    {
        GameObject hexagone = obj.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = true;
        hexagone.GetComponent<SpriteRenderer>().color = Color.blue;
    }
    public void ChangeHexagoneColorToWhite(GameObject obj)
    {
        GameObject hexagone = obj.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = false;
        hexagone.GetComponent<SpriteRenderer>().color = Color.white;
    }

    
  

    public void AfficherStats()
    {
        fenetre.SetActive(true);
        stats.GetComponent<Text>().text = "Enemy\n----------------\nPV : " + currentPv + "/" + MaxPv+"\nAttaque : "+attack+"\nDefense : "+defense;
    }

    public void EnleverStats()
    {
        fenetre.SetActive(false);
    }
}
