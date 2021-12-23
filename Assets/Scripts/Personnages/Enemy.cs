using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int MaxPv = 100;
    private int currentPv;
    private int attack = 20;
    private int defense = 30;
    private int reach = 1;
    private bool isSelected;
    private bool attacked = false;
    private int pa = 5;
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



    // Start is called before the first frame update

    void Start()
    {
        this.currentPv = MaxPv;
        isSelected = false;
 
        boutonAvancer = GameObject.Find("Button Avance");
        Debug.Log(boutonAvancer);
    }

    // Update is called once per frame
    public void Update()
    {
        if(currentPath != null && Vector3.Distance(ennemy.transform.position, movePoint.position) == 0 && launchMove == true && pa > 0){
            
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            pa = pa - 1;
            

            if (currentPath.Count == 1){
                Debug.Log("Fin des haricots");
                map.paEnemy = map.paEnemy - map.j + 1;
                currentPath = null;
                launchMove = false;
                
                //target.GetComponent<Renderer>().material.color = new Color(0.5849056f, 0.5403813f, 0.4773051f, 1);

            }
        } 
        else if(pa == 0) {
            launchMove = false;
            pa = 5;
        }

    }


    public void Move(){
        if ( map.action==true) //v�rification nombre de pa
        {
  
            launchMove = true;
  
       }
      

        //mettre la v�rification de la distance dans une fonction de au clic sur la case et non le bouton
        //permettre donc d'interdire cette fonction de clic sur une case lorsque launchMove est true
    }
    
    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
        if (!isSelected)
        {
            ChangeHexagoneColorToWhite(this);
        }
    }
    public void SetIsSelectedObject2(bool selected)
    {
        isSelected = selected;
        if (isSelected)
        {
            ChangeHexagoneColorToBleu(this);
        }
        else
        {
            ChangeHexagoneColorToWhite(this);
        }
    }
    private void IsDead()
    {
        if (currentPv <= 0)
        {
            this.currentPv = 0;
            this.gameObject.SetActive(false);
            ChangeHexagoneColorToWhite(this);
            attacked = false;
        }
    }
    public bool IsAttacked(int damage)
    {
        if (currentPv > 0)
        {
            this.currentPv -= (damage-defense);
            IsDead();
            ChangeHexagoneColorToWhite(this);
            attacked = true;
            isSelected = false;
        }
        return attacked;

    }

    public void ChangeHexagoneColorToBleu(Enemy obj)
    {
        GameObject hexagone = obj.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = true;
        hexagone.GetComponent<SpriteRenderer>().color = Color.blue;
    }
    public void ChangeHexagoneColorToWhite(Enemy obj)
    {
        GameObject hexagone = obj.transform.GetChild(0).gameObject;
        hexagone.GetComponent<SpriteRenderer>().enabled = false;
        hexagone.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public bool GetAttacked()
    {
        return attacked;
    }
    public void ChangeAttacked(bool attacked)
    {
        this.attacked = attacked;
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
