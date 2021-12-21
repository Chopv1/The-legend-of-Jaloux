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
    private int pa;



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
        pa = 5;

        boutonAvancer = GameObject.Find("Button Avance");
        Debug.Log(boutonAvancer);
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentPath != null){
            Debug.Log("Avance ennemy !");
            Move();
            int currNode = 0;

            while (currNode < currentPath.Count - 1 && currNode < pa - 1){
                Debug.Log("Enemy : " + currNode);
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y);
                Debug.Log("Start : " + start);
                Debug.Log("End : " + end);

                Debug.DrawLine(start, end, Color.red);

        
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



        if(currentPath != null && Vector3.Distance(ennemy.transform.position, movePoint.position) == 0 && launchMove == true){
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            

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
        GameObject fenetre = this.transform.GetChild(0).gameObject;
        fenetre.GetComponent<SpriteRenderer>().enabled = true;
        GameObject stats = GameObject.Find("Stats");
        stats.GetComponent<Text>().enabled = true;
        stats.GetComponent<Text>().text = "Stats\n----------------\nPV : " + currentPv + "/" + MaxPv+"\nAttaque : "+attack+"\nD�fense : "+defense;
    }
}
