using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public GameObject unit;
    public TileType[] tileTypes;

    public int[,] tiles;
    Node[,] graph;

    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start() {
        //unit.GetComponent<Unit>().tileX = (int)unit.transform.position.x;
        //unit.GetComponent<Unit>().tileY = (int)unit.transform.position.y;
        unit.GetComponent<Unit>().map = this;
        
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

        tiles[4,4] = 1;
        tiles[4,5] = 1;
        tiles[4,6] = 1;
        tiles[5,6] = 1;
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

    public void GeneratePathTo(int x, int y){

        unit.GetComponent<Unit>().currentPath = null;

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

        foreach(Node v in graph){
            if(v != source){
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while(unvisited.Count > 0){
            Node closer = null;

            foreach(Node possible in unvisited){
                if(closer == null || dist[possible] < dist[closer]){
                    closer = possible;
                }
            }

            if(closer == target){
                break;
            }

            unvisited.Remove(closer);

            foreach(Node v in closer.neighbours){
                //float totDist = dist[closer] + closer.DistantTo(v);
                float totDist = dist[closer] + CostToEnterTile(v.x, v.y);
                if(totDist < dist[v]){
                    dist[v] = totDist;
                    prev[v] = closer;
                }
            }
        }
        if(prev[target] == null){
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while(curr != null){
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        unit.GetComponent<Unit>().currentPath = currentPath;
    }
    
}
