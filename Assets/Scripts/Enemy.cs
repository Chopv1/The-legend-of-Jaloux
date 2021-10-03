using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;
    public int defense = 3;

    private bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetIsSelected()
    {
        if (isSelected)
        {
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
    }
}
