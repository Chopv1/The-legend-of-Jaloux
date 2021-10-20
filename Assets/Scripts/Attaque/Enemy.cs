using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxPv = 100;
    private int currentPv;
    private float attack;
    private int reach;
    private bool isSelected;
    // Start is called before the first frame update
    
    void Start()
    {
        this.currentPv = MaxPv;
        isSelected = false;
    }

    // Update is called once per frame
    public void Update()
    {
 
    }
    public void SetIsSelected()
    {
        if (this.isSelected)
        {
            this.isSelected = false;
            ChangeHexagoneColorToWhite(this);
        }
        else
        {
            this.isSelected = true;
        }
    }
    public void SetIsSelectedObject2()
    {
        if (this.isSelected)
        {
            this.isSelected = false;
            ChangeHexagoneColorToWhite(this);
        }
        else
        {
            this.isSelected = true;
            ChangeHexagoneColorToBleu(this);
        }
    }
    private void IsDead()
    {
        if (currentPv <= 0)
        {
            this.currentPv = 0;
            this.gameObject.SetActive(false);
            ChangeHexagoneColorToWhite(this);
        }
    }
    public void IsAttacked(int damage)
    {
        this.currentPv -=damage;
        IsDead();

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
}
