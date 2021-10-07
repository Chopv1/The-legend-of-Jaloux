using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask enemyLayer;
    public int MaxPv=100;
    public MouseManager mouse;

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
             Collider2D[] hitInfo = Physics2D.OverlapCircleAll(this.transform.position, reach, enemyLayer);
            
            foreach(Collider2D hit in hitInfo)
            {
                GameObject hexagone = hit.transform.GetChild(0).gameObject;
                hexagone.GetComponent<SpriteRenderer>().enabled = true;
                hexagone.GetComponent<SpriteRenderer>().color = Color.black;
            }
            Vector2 rayCastPos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D obj = Physics2D.Raycast(rayCastPos, Vector2.zero, enemyLayer);
            if (Input.GetMouseButtonDown(0) && obj.transform.CompareTag("Enemy") && IsInReach(obj.transform.gameObject))
            {
                obj.transform.gameObject.GetComponent<Enemy>().IsAttacked(attack);
                Debug.Log("-"+attack);
                this.SetIsSelected();
                mouse.GetComponent<MouseManager>().setHeroSelected();
                foreach (Collider2D hit in hitInfo)
                {
                    GameObject hexagone = hit.transform.GetChild(0).gameObject;
                    hexagone.GetComponent<SpriteRenderer>().enabled = false;
                    hexagone.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                foreach(Collider2D hit in hitInfo)
                {
                    GameObject hexagone = hit.transform.GetChild(0).gameObject;
                    hexagone.GetComponent<SpriteRenderer>().enabled = false;
                    hexagone.GetComponent<SpriteRenderer>().color = Color.white;
                }
                this.SetIsSelected();
            }
        }
    }
    
    private bool IsInReach(GameObject obj)
    {
        bool itIsInReach = false;
        float x = obj.transform.position.x - reach;
        float y = obj.transform.position.y - reach;
        if (x<=this.transform.position.x || y<= this.transform.position.y)
        {
            itIsInReach = true;
        }

        return itIsInReach;
    }
    public bool getSelected()
    {
        return this.isSelected;
    }
}
