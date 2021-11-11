using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;
    private GameObject[] tableauPositionEnemy;
    public GameObject mouseManagerObject;
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
        tableauPositionEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy910 in tableauPositionEnemy)
        {
            if(enemy910.transform.position.x == tileX && enemy910.transform.position.y == tileY)
            {   
                this.GetComponent<BoxCollider>().enabled=false;
            }
        }  
    }
}