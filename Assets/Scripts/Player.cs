using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int pv;
    private float attack;
    private int reach = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CanAttack();
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
}
