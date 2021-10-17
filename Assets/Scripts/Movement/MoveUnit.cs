using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit
{
    public Unit unit;
    public GameObject player;
    public Transform movePoint;
    public TileMap map;

    public MoveUnit(Unit unit, GameObject player, Transform movePoint, TileMap map){
        this.unit = unit;
        this.player = player;
        this.movePoint = movePoint;
        this.map = map;
    }

    void Update()
    {
        
    }
}
