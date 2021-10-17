using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
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



    public List<Node> currentPath = null;

    void Start()
    {

        compteurPA = GameObject.Find("Compteur PA");
        Debug.Log(compteurPA);

        compteurPA.GetComponent<Text>().enabled = true;
        

        boutonAvancer = GameObject.Find("Button Avance");
        Debug.Log(boutonAvancer);

        boutonFinTour = GameObject.Find("Button FinTour");
        Debug.Log(boutonFinTour);

        movePoint.GetComponent<Renderer>().enabled = false;
    }

    void Update(){

        print(map.pa);
        if (currentPath != null){
            int currNode = 0;



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

            }
        }
        compteurPA.GetComponent<Text>().text = "PA : " + map.pa.ToString();
    }


    public void Move(){
        if ( map.action==true) //vérification nombre de pa
        {
  
            launchMove = true;
  
       }
      

        //mettre la vérification de la distance dans une fonction de au clic sur la case et non le bouton
        //permettre donc d'interdire cette fonction de clic sur une case lorsque launchMove est true
    }
}
