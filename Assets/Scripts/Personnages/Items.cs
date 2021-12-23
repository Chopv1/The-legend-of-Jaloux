using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string nomItem;
    public string typeItem;
    public int valeurAttributs;

    public Items(string nomItem, string typeItem, int valeurAttributs)
    {
        if (typeItem.Equals("soin") || typeItem.Equals("bottes") || typeItem.Equals("armure") || typeItem.Equals("casque")) ;
        this.nomItem = nomItem;
        this.typeItem = typeItem;
        this.valeurAttributs = valeurAttributs;
    }

    public string getNomItem()
    {
        return this.nomItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
