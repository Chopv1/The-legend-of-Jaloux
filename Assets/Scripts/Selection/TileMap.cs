using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TileMap : MonoBehaviour
{
    public Unit unit;
    public Enemy enemy;
    public TileType[] tileTypes;
    public ClickableTile target;
    public Material appearance;
    public GameObject pathPlayer;
    public GameObject pathEnemy;
    public MouseManager reset;
    public bool action;
    public int pa = 10;
    public int paEnemy = 5;
    public int i = 0;
    public int j = 0;





    public int[,] tiles;
    Node[,] graph;

    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start() {
        //unit.GetComponent<Unit>().tileX = (int)unit.transform.position.x;
        //unit.GetComponent<Unit>().tileY = (int)unit.transform.position.y;
        unit.GetComponent<Unit>().map = this;
        enemy.GetComponent<Enemy>().map = this;
        GenerateMapData();
        GeneratePathFfindingGraph();
        GenerateMapVisual();


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


    void GeneratePathFfindingGraph(){
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

    void GenerateMapVisual(){
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y){
        return(new Vector3(x, y, 0));
    }

    public void GeneratePathTo(int x, int y)
    {
        if (unit.launchMove == false)
        {
         
              
            unit.GetComponent<Unit>().target = this.target;
            unit.GetComponent<Unit>().currentPath = null;
            enemy.GetComponent<Enemy>().currentPath = null;

            if (unit.GetComponent<Unit>().target.GetComponent<Transform>().position == unit.GetComponent<Unit>().GetComponent<Transform>().position)
            {
                unit.boutonAvancer.GetComponent<Button>().interactable = false;

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
                unit.boutonAvancer.GetComponent<Button>().interactable = false;
            }
            else
            {
                pathPlayer.GetComponent<Renderer>().material.color = Color.white;
                action = true;
                unit.boutonAvancer.GetComponent<Button>().interactable = true;

            }


            currentPath.Reverse();

            unit.GetComponent<Unit>().currentPath = currentPath;
            
        }

        if (enemy.launchMove == false)
        {

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

    public void finirTour()
    {
        if (unit.launchMove == false)
        {
            pa = 10;
            unit.GetComponent<Unit>().currentPath = null;
            unit.boutonAvancer.GetComponent<Button>().interactable = false;
            unit.boutonFouille.GetComponent<Button>().interactable = true;
            reset.GetComponent<MouseManager>().CanMove(false);
        }
    }


}
