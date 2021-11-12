using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public LayerMask enemyLayer;
    public int MaxPv=100;
    public MouseManager mouse;

    private Camera cam;
    private int currentPv;
    private int attack;
    private int defense;
    private float reach;
    private bool isSelected;
    private int pa;


    // Start is called before the first frame update
    void Start()
    {
        currentPv = MaxPv;
        cam = Camera.main;
        isSelected = false;
        attack = 70;
        reach = 1f;
        pa = 10;
        defense = 50;
    }

    // Update is called once per frame
    public void Update()
    {
        if (isSelected)
        {
            CanAttack();
        }
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
        
    }
    public void CanAttack()
    {
        Debug.Log("Attak");

        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);

        ChangeHexagoneColorToBlack(hitInfo);

        Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D obj = Physics2D.Raycast(rayCastPos, Vector2.zero, enemyLayer);
        if (Input.GetMouseButtonDown(0) && obj.collider != null && obj.transform.gameObject.CompareTag("Enemy") && IsInReach(obj.transform.gameObject))
        {
            obj.transform.gameObject.GetComponent<Enemy>().IsAttacked(attack);
            pa -= 1;
            Debug.Log("Attaque");
            ChangeHexagoneColorToWhite(hitInfo);
            mouse.GetComponent<MouseManager>().ClearSelection();
        }

        if (Input.GetMouseButtonDown(0) && obj.collider == null)
        {
            ChangeHexagoneColorToWhite(hitInfo);
            mouse.GetComponent<MouseManager>().ClearSelection();
        }
        
    }
    
    public bool IsInReach(GameObject obj)
    {
        bool reachable = false;
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
        foreach(Collider2D hit in hitInfo)
        {
            if(hit.gameObject == obj)
            {
                reachable = true;
            }
        }
        return reachable;
    }
    public bool GetSelected()
    {
        return this.isSelected;
    }

    private void ChangeHexagoneColorToWhite(Collider2D[] hitInfo)
    {
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = false;
            hexagone.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void ChangeHexagoneColorToBlack(Collider2D[] hitInfo)
    {
        foreach (Collider2D hit in hitInfo)
        {
            GameObject hexagone = hit.transform.GetChild(0).gameObject;
            hexagone.GetComponent<SpriteRenderer>().enabled = true;
            hexagone.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    public void AfficherStats()
    {
        GameObject fenetre = this.transform.GetChild(1).gameObject;
        fenetre.GetComponent<SpriteRenderer>().enabled = true;
        GameObject stats = GameObject.Find("Stats");
        stats.GetComponent<Text>().enabled = true;
        stats.GetComponent<Text>().text = "Stats\n----------------\nPV : " + currentPv + "/" + MaxPv + "\nAttaque : " + attack + "\nD�fense : " + defense+"\nPA : "+pa;
    }
}
