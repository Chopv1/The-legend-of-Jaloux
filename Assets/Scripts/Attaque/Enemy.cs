using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int MaxPv = 100;
    private int currentPv;
    private int attack = 20;
    private int defense = 30;
    private int reach = 1;
    private bool isSelected;
    private bool attacked = false;

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
            attacked = false;
        }
    }
    public bool IsAttacked(int damage)
    {
        if (currentPv > 0)
        {
            this.currentPv -= (damage-defense);
            IsDead();
            ChangeHexagoneColorToWhite(this);
            attacked = true;
            isSelected = false;
        }
        return attacked;

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

    public bool GetAttacked()
    {
        return attacked;
    }
    public void ChangeAttacked(bool attacked)
    {
        this.attacked = attacked;
    }

    public void AfficherStats()
    {
        GameObject fenêtre = this.transform.GetChild(1).gameObject;
        fenêtre.GetComponent<SpriteRenderer>().enabled = true;
        GameObject stats = GameObject.Find("Stats");
        stats.GetComponent<Text>().enabled = true;
        stats.GetComponent<Text>().text = "Stats\n----------------\nPV : " + currentPv + "/" + MaxPv+"\nAttaque : "+attack+"\nDéfense : "+defense;
    }
}
