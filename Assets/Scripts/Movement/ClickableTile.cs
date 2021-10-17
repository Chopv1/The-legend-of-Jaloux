using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;
    void OnMouseUp(){
        Debug.Log("Click!");
        map.target = this;

        if(map.tiles[tileX, tileY] != 1){
            //gameObject.GetComponent<Renderer>().material.color = Color.red;
            map.GeneratePathTo(tileX, tileY);
        }

        

    }

    void Update()
    {
        
    }
}
