using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCarte : MonoBehaviour

{
    public string title;
    public string description;
    public int pointAction;
   // public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTitle (string nouveauTitle)
    {
        this.title = nouveauTitle;
        
    }

    public void setDescription(string description)
    {
        this.title = description;

    }

    public void setPointAction(string pointAction)
    {
        this.title = pointAction;

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
