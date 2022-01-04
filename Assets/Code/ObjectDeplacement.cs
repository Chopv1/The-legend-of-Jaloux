using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectDeplacement : MonoBehaviour
{
    public GameObject elementDeplace;
    private Camera cam;

    public int Y_clic_carré;
    public int X_clic_carré;
    public Boolean estEnMouvement = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ChangementCible();
        if (estEnMouvement == false)
            deplacement();
    }

    void ChangementCible()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float X_clic = Input.mousePosition.x;
            float Y_clic = Input.mousePosition.y;
            Vector3 sourisPosition = cam.ScreenToWorldPoint(new Vector3(X_clic, Y_clic, 10f));
            X_clic_carré = (int)Math.Round(sourisPosition.x, MidpointRounding.ToEven);
            Y_clic_carré = (int)Math.Round(sourisPosition.y, MidpointRounding.ToEven);
        }

    }
    void deplacement()
    {
        estEnMouvement = true;
        if (elementDeplace.transform.position.x < X_clic_carré)
        {
            elementDeplace.transform.position = new Vector2(elementDeplace.transform.position.x + 1, elementDeplace.transform.position.y);
        }
        else if (elementDeplace.transform.position.x > X_clic_carré)
        {
            elementDeplace.transform.position = new Vector2(elementDeplace.transform.position.x - 1, elementDeplace.transform.position.y);
        }
        else if (elementDeplace.transform.position.y < Y_clic_carré)
        {
            elementDeplace.transform.position = new Vector2(elementDeplace.transform.position.x, elementDeplace.transform.position.y + 1);
        }
        else if (elementDeplace.transform.position.y > Y_clic_carré)
        {
            elementDeplace.transform.position = new Vector2(elementDeplace.transform.position.x - 1, elementDeplace.transform.position.y - 1);
        }

        if (elementDeplace.transform.position.x != X_clic_carré || elementDeplace.transform.position.y != Y_clic_carré)
            Invoke("deplacement", 2);
        else
            estEnMouvement = false;

    }
}
