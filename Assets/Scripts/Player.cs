using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int pv;
    private int attack = 5;
    private int reach = 1;
    private bool isSelected = false;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanAttack();
    }

    public void setIsSelected()
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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));
        RaycastHit hitInfo; 

        if(Physics.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), out hitInfo)&& (Input.mousePosition.x<=reach&&Input.mousePosition.y<= reach))
        {
            Debug.Log("Can attack");
        }
    }
    public void IsSelected()
    {

    }

    public void Infliger()
    {
        Debug.Log("Les pv ennemis étaient de : " + enemy.hp);
        enemy.hp -= attack - enemy.defense;
        Debug.Log("Les pv ennemis sont maintenant de : " + enemy.hp); 
    }
}
