using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TileMap : MonoBehaviour
{
    public GameObject prefEnemi;
    public GameObject prefEnemiMouv;
    public GameObject prefEnemiPath;


    public GameObject unit;
    public List<GameObject> enemies;
    public TileType[] tileTypes;
    public ClickableTile target;
    public Material appearance;
    public GameObject pathPlayer;
    public GameObject pathEnemy;
    public MouseManager reset;
    public bool action;
    public int pa = 10;
    public int paEnemy;
    public int i = 0;
    public int j = 0;
    public string nom;
    public GameObject pos;
    int compteur = 0;

    public LayerMask eL;

    public int[,] tiles;
    Node[,] graph;

    int mapSizeX = 11;
    int mapSizeY = 11;

    void Start() {

        enemies = new List<GameObject>();
        nom = "Map 1";
        pos = null;
        //unit.GetComponent<Unit>().tileX = (int)unit.transform.position.x;
        //unit.GetComponent<Unit>().tileY = (int)unit.transform.position.y;
        unit.GetComponent<Unit>().map = this;
        
        GenerateMapData();
        GeneratePathFfindingGraph();
        GenerateMapVisual();


    }
    private void Update()
    {
        this.unit.transform.position=unit.transform.position;
    }

    void GenerateMapData(){
        tiles = new int[mapSizeX, mapSizeY];

        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                tiles[x, y] = 0;
            }
        }

        tiles[4, 4] = 1;
        tiles[4, 5] = 1;
        tiles[4, 6] = 1;
        tiles[5, 6] = 1;
        tiles[6, 6] = 1;
        tiles[6, 5] = 1;
        tiles[6, 4] = 1;
    }

    
    float CostToEnterTile(int x, int y){
        TileType type = tileTypes[tiles[x, y]];
        return type.movementCost;
    }

    void GeneratePathFfindingGraph()
    {
        Vector3 posCentreSalle = new Vector3(0,0,0);
        if (pos!=null)
        {
            posCentreSalle = VerifyCenter(pos);
        }

        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                }
                if (y > 0)
                {
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                }
            }
        }
    }


    void GenerateMapVisual(){

        Vector3 posCentreSalle = new Vector3(0,0,0);
        if (pos!=null)
        {
            posCentreSalle = VerifyCenter(pos);
        }
        int nAl = Random.Range(1, 5);
        for (int x = 0; x < mapSizeX; x++){ void GeneratePathFfindingGraph(){
        graph = new Node[mapSizeX, mapSizeY];

            for(int x = 0; x < mapSizeX; x++){
                for(int y = 0; y < mapSizeY; y++){
                    graph[x, y] = new Node();
                    graph[x, y].x = x;
                    graph[x, y].y = y;
                }
            }

            for(int x = 0; x < mapSizeX; x++){
                for(int y = 0; y < mapSizeY; y++){
                    if(x > 0){
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                    }
                    if(x < mapSizeX - 1){
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                    }
                    if(y > 0){
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                    }
                    if(y < mapSizeY - 1){
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                    }
                }
            }
        }
            for(int y = 0; y < mapSizeY; y++){
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x+ posCentreSalle.x, y+posCentreSalle.y, 0), Quaternion.identity);
                GenerationEnnemi(x, y, nAl, posCentreSalle);
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
            this.transform.position += new Vector3(posCentreSalle.x, posCentreSalle.y, 0);
        }
        compteur = 0;
        foreach(GameObject e in enemies)
        {
            e.GetComponent<Enemy>().map = this;
        }
    }
    public void GenerationEnnemi(int x, int y, int nAl, Vector3 pos)
    {
        int e = Random.Range(0, 10);
        if(e==1 && compteur <= nAl && tiles[x,y]!=1)
        {
            GameObject es= (GameObject)Instantiate(prefEnemi, new Vector3(x + pos.x, y + pos.y, 0), Quaternion.identity);
            enemies.Add(es);
            compteur += 1;
        }
    }
    public Vector3 TileCoordToWorldCoord(int x, int y){
        Vector3 posCentreSalle = new Vector3(0,0,0);
        if (pos!=null)
        {
            posCentreSalle = VerifyCenter(pos);
        }
        
        return(new Vector3(x + posCentreSalle.x, y + posCentreSalle.y, 0));
    }

    public void GeneratePathTo(int x, int y)
    {
        
        if (unit.GetComponent<Unit>().launchMove == false)
        {
         
              
            unit.GetComponent<Unit>().target = this.target;
            unit.GetComponent<Unit>().currentPath = null;
            foreach(GameObject enemy in enemies) {
                enemy.GetComponent<Enemy>().currentPath = null;
            }
            

            if (unit.GetComponent<Unit>().target.GetComponent<Transform>().position == unit.GetComponent<Unit>().GetComponent<Transform>().position)
            {
                unit.GetComponent<Unit>().boutonAvancer.GetComponent<Button>().interactable = false;

            }

            //Création du chemin pour le joueur
            Dictionary<Node, float> dist = new Dictionary<Node, float>();
            Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

            List<Node> unvisited = new List<Node>();


            Node source = graph[
                unit.GetComponent<Unit>().tileX,
                unit.GetComponent<Unit>().tileY
                ];

            Node target = graph[x, y];



            dist[source] = 0;
            prev[source] = null;

            foreach (Node v in graph)
            {
                if (v != source)
                {
                    dist[v] = Mathf.Infinity;
                    prev[v] = null;
                }
                unvisited.Add(v);
            }

            while (unvisited.Count > 0)
            {
                Node closer = null;

                foreach (Node possible in unvisited)
                {
                    if (closer == null || dist[possible] < dist[closer])
                    {
                        closer = possible;
                    }
                }

                if (closer == target)
                {
                    break;
                }

                unvisited.Remove(closer);

                foreach (Node v in closer.neighbours)
                {
                    //float totDist = dist[closer] + closer.DistantTo(v);
                    float totDist = dist[closer] + CostToEnterTile(v.x, v.y);
                    if (totDist < dist[v])
                    {
                        dist[v] = totDist;
                        prev[v] = closer;
                    }
                }
            }
            if (prev[target] == null)
            {
                return;
            }

             i = 0;
             List<Node> currentPath = new List<Node>();

            Node curr = target;
          
            while (curr != null)
            {
                i++;
                currentPath.Add(curr);
                curr = prev[curr];

            }
            

            if (i > pa+1)
            {
                print("vous n avez pas assez de pa");
                pathPlayer.GetComponent<Renderer>().material.color = Color.red;
                action = false;
                unit.GetComponent<Unit>().boutonAvancer.GetComponent<Button>().interactable = false;
            }
            else
            {
                pathPlayer.GetComponent<Renderer>().material.color = Color.white;
                action = true;
                unit.GetComponent<Unit>().boutonAvancer.GetComponent<Button>().interactable = true;

            }


            currentPath.Reverse();

            unit.GetComponent<Unit>().currentPath = currentPath;
            
        }

        foreach(GameObject enemy in enemies) {



            if (enemy.GetComponent<Enemy>().launchMove == false)
            {
                Debug.Log(enemy + " path !");
                //Création du chemin pour l'enemy
                Dictionary<Node, float> dist2 = new Dictionary<Node, float>();
                Dictionary<Node, Node> prev2 = new Dictionary<Node, Node>();

                List<Node> unvisited2 = new List<Node>();


                Node source2 = graph[
                    enemy.GetComponent<Enemy>().tileX,
                    enemy.GetComponent<Enemy>().tileY
                    ];

                Node target2 = graph[x, y];



                dist2[source2] = 0;
                prev2[source2] = null;

                foreach (Node v in graph)
                {
                    if (v != source2)
                    {
                        dist2[v] = Mathf.Infinity;
                        prev2[v] = null;
                    }
                    unvisited2.Add(v);
                }

                while (unvisited2.Count > 0)
                {
                    Node closer = null;

                    foreach (Node possible in unvisited2)
                    {
                        if (closer == null || dist2[possible] < dist2[closer])
                        {
                            closer = possible;
                        }
                    }

                    if (closer == target2)
                    {
                        break;
                    }

                    unvisited2.Remove(closer);

                    foreach (Node v in closer.neighbours)
                    {
                        //float totDist = dist2[closer] + closer.DistantTo(v);
                        float totDist = dist2[closer] + CostToEnterTile(v.x, v.y);
                        if (totDist < dist2[v])
                        {
                            dist2[v] = totDist;
                            prev2[v] = closer;
                        }
                    }
                }
                if (prev2[target2] == null)
                {
                    return;
                }

                j = 0;
                List<Node> currentPath2 = new List<Node>();

                Node curr2 = target2;
            
                while (curr2 != null)
                {
                    j++;
                    currentPath2.Add(curr2);
                    curr2 = prev2[curr2];

                }
                
                currentPath2.Reverse();

                currentPath2.RemoveAt(currentPath2.Count - 1);

                enemy.GetComponent<Enemy>().currentPath = currentPath2;
                
            }
        }
    }

    public void finirTour()
    {
        if (unit.GetComponent<Unit>().launchMove == false)
        {
            pa = 10;
            unit.GetComponent<Unit>().currentPath = null;
            unit.GetComponent<Unit>().boutonAvancer.GetComponent<Button>().interactable = false;
            unit.GetComponent<Unit>().boutonFouille.GetComponent<Button>().interactable = true;
            reset.GetComponent<MouseManager>().CanMove(false);
            reset.GetComponent<MouseManager>().ClearSelection();
        }
    }
    /// 
    /// 
    ///  GENERATION D'UNE SALLE
    ///
    /// 
    public void GenerationSalle(GameObject centreSalle)
    {
        APlusEnnemi(centreSalle);
        this.pos = centreSalle;
        GenerateMapData();
        GeneratePathFfindingGraph();
        GenerateMapVisual();
        target.ChangeMap(this);
    }
    ///Pour enlever les ennemies de la map mais on les garde en info dans la salle
    public void APlusEnnemi(GameObject centreSalle)
    {
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject e in ennemis)
        {
            centreSalle.GetComponent<InfoCentreSalle>().ennemi.Add(e);
            e.SetActive(false); 
        }
    }
    public void ReEnnemi(GameObject centreSalle)
    {
        foreach(GameObject e in centreSalle.GetComponent<InfoCentreSalle>().ennemi)
        {
            e.SetActive(true);
        }
    }
    public Vector3 VerifyCenter(GameObject centreSalle)
    {
        Vector3 posCentreSalle = centreSalle.transform.position;

        GameObject door = centreSalle.GetComponent<InfoCentreSalle>().Porte ;
        int nbDoor = door.GetComponent<HeroCreationSalle>().ouverture;

        switch(nbDoor)
        {
            case 1:
                posCentreSalle += new Vector3(0, -5.5f, 0);
                posCentreSalle = new Vector3(0, posCentreSalle.y, 0);
                Debug.Log("Bottom Door");
                
                break;
            case 2:
                posCentreSalle += new Vector3(-5.5f, 0, 0);
                posCentreSalle = new Vector3(posCentreSalle.x, 0, 0);
                Debug.Log("Left Door");
                break;
            case 3:
                posCentreSalle += new Vector3(0, -5.5f, 0);
                posCentreSalle = new Vector3(0, posCentreSalle.y, 0);
                Debug.Log("Top Door");
                break;
            case 4:
                posCentreSalle += new Vector3(-4.5f, 0, 0);
                posCentreSalle = new Vector3(posCentreSalle.x, 0, 0);
                Debug.Log("Right Door");
                
                break;
        }



        return posCentreSalle;
    }

    public void TPhero(GameObject nouvPos, int ouverture)
    {
        List<GameObject> porte= new List<GameObject>();
        int offsetX = 0;
        int offsetY = 0;

        Debug.Log("Je bouge" + unit.transform.position + nouvPos);
        GameObject lesPortes = nouvPos.transform.GetChild(2).gameObject;
       
        for(int i=0;  i<=lesPortes.transform.childCount-1 ;i++)
        {
            porte.Add(lesPortes.transform.GetChild(i).gameObject);
        }

        switch (ouverture)
        { // 
            case 1:
                ouverture = 3;// demande ouverture pour haut donc ouverture par le bas  
                offsetY = -10;
                break;
            case 2:
                ouverture = 4; // demande ouverture par la gauche donc ouverture par la droite
                offsetX = 10;
                break;
            case 3:
                ouverture = 1; // demande ouverture pour bas donc ouverture par le haut
                offsetY = 10;
                break;
            case 4:
                offsetX = -10;
                ouverture = 2; // demade ouverture par la droite donc ouverture par la gauche

                break;
            default:
                break;

        }

        foreach(GameObject p in porte)
        {
            if(p.GetComponent<HeroCreationSalle>().ouverture==ouverture)
            {
                unit.transform.position = p.transform.position;
                unit.GetComponent<Unit>().tileX = unit.GetComponent<Unit>().tileX + offsetX;
                unit.GetComponent<Unit>().tileY = unit.GetComponent<Unit>().tileY + offsetY;
                Update();
                Debug.Log("J'ai bougé" + unit.transform.position);
                
            }
        }

        
    }

}
