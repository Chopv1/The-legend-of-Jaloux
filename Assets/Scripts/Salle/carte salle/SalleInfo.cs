using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Scripts Object/cartes/SalleInfo", menuName = "ScriptableObject / Salle")]
public class SalleInfo : ScriptableObject
{
    
    
    public string title;
    public string description;
    public int pointAction;
    public GameObject salle;

    public void ChoisirCarte( GeneratorCarte salle)
    {
        title = salle.title;
        description = salle.description;
        pointAction = salle.pointAction;
       
       

    }


}
