using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxPv = 100;
    private int currentPv;
    private float attack;
    private int reach;
    private bool isSelected = false;
    // Start is called before the first frame update
    
    void Start()
    {
        this.currentPv = MaxPv;
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
        }
        else
        {
            this.isSelected = true;
        }
    }
    private void IsDead()
    {
        if (currentPv <= 0)
        {
            this.currentPv = 0;
            this.gameObject.SetActive(false);
        }
    }
    public void IsAttacked(int damage)
    {
        this.currentPv -=damage;
        IsDead();
    }
}
