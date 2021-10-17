using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask enemyLayer;
    public int MaxPv=100;
    public MouseManager mouse;
    public Transform posiitonHero;

    private Camera cam;
    private int currentPv;
    private int attack = 40;
    private float reach = 1f;
    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPv = MaxPv;
        cam = Camera.main;
    }

    // Update is called once per frame
    public void Update()
    {
        CanAttack();
    }

    public void SetIsSelected()
    {
        if(isSelected)
        {
            mouse.GetComponent<MouseManager>().ClearSelection();
            isSelected = false;
            
        }
        else
        {
            isSelected = true;
        }
    }
    public void CanAttack()
    {
        if(isSelected)
        {
             Collider2D[] hitInfo = Physics2D.OverlapCircleAll(posiitonHero.position, reach, enemyLayer);

            ChangeHexagoneColorToBlack(hitInfo);

            Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D obj = Physics2D.Raycast(rayCastPos, Vector2.zero, enemyLayer);

            if (Input.GetMouseButtonDown(0)&& obj.collider != null && obj.transform.gameObject.CompareTag("Enemy") && IsInReach(obj.transform.gameObject))
            {
                obj.transform.gameObject.GetComponent<Enemy>().IsAttacked(attack);
                this.SetIsSelected();
                ChangeHexagoneColorToWhite(hitInfo);
            }
            if(Input.GetMouseButtonDown(0) && obj.collider != null && !obj.transform.gameObject.CompareTag("Enemy"))
            {
                ChangeHexagoneColorToWhite(hitInfo);
                this.SetIsSelected();
            }
        }
        else
        {
            Collider2D[] hitInfo = Physics2D.OverlapCircleAll(posiitonHero.position, reach, enemyLayer);
            ChangeHexagoneColorToWhite(hitInfo);
        }
    }
    
    private bool IsInReach(GameObject obj)
    {
        bool reachable = false;
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(posiitonHero.position, reach, enemyLayer);
        foreach(Collider2D hit in hitInfo)
        {
            if(hit.gameObject == obj)
            {
                reachable = true;
            }
        }
        return reachable;
    }
    public bool getSelected()
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
}
