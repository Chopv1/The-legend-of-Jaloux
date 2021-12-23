using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject selection;
    public int MaxPv = 100;



    private int currentPv;
    private int attack = 20;
    private int defense = 30;
    private int reach = 1;
    private bool isSelected;

    public int CurrentPv { get => currentPv; set => currentPv = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Reach { get => reach; set => reach = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }


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
    
    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
        if (!isSelected)
        {
            ChangeHexagoneColorToWhite(this);
        }
    }
    public void SetIsSelectedObject2(bool selected)
    {
        isSelected = selected;
        if (isSelected)
        {
            ChangeHexagoneColorToBleu(this);
        }
        else
        {
            ChangeHexagoneColorToWhite(this);
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
        if (currentPv > 0)
        {
            this.currentPv -= (damage-defense);
            Debug.Log("Enemy Attacked");
            IsDead();
            ChangeHexagoneColorToWhite(this);
            isSelected = false;
        }

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

    
  

    public void AfficherStats()
    {
        GameObject fenetre = this.transform.GetChild(0).gameObject;
        fenetre.GetComponent<SpriteRenderer>().enabled = true;
        GameObject stats = GameObject.Find("Stats");
        stats.GetComponent<Text>().enabled = true;
        stats.GetComponent<Text>().text = "Stats\n----------------\nPV : " + currentPv + "/" + MaxPv+"\nAttaque : "+attack+"\nD�fense : "+defense;
    }
    
    
}
