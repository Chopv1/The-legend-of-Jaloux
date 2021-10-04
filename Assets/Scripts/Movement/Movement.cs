using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;
    public Unit unit;
    public float moveDelay = 0f;
    public float nextMove;
    void Start()
    {
        movePoint.position = transform.position;
    }



    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime * 30);
        if (Vector3.Distance(transform.position, movePoint.position) == 0 && unit.currentPath != null){
        }
        /*if (Vector3.Distance(transform.position, movePoint.position) == 0){
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Time.time > nextMove){
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && Time.time > nextMove){
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }

            if (Input.GetMouseButtonDown(0)){
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                movePoint.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y, 0f);
            }
        }*/
    }


}
