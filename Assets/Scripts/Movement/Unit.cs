using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;
    public GameObject player;
    public Transform movePoint;
    public bool launchMove = false;

    public List<Node> currentPath = null;

    void Update(){
        if(currentPath != null){
            int currNode = 0;
            
            while(currNode < currentPath.Count - 1){
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }

        if(currentPath != null && Vector3.Distance(player.transform.position, movePoint.position) == 0 && launchMove == true){
            currentPath.RemoveAt(0);
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;

            if(currentPath.Count == 1){
                currentPath = null;
                launchMove = false;
            }
        }
    }



    public void Move(){
        launchMove = true;
    }
}
