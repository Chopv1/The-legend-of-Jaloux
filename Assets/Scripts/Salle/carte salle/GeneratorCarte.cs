using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCarte : MonoBehaviour

{
    public GameObject[] portes;
    public int type;
    public int rotation;
    public int[] signature = new int[4]; // [ H,D,B,G]
    public string title;
    public string description;
    public int pointAction;

    // public GameObject gameObject;

    // Start is called before the first frame update

    void Start()
    {
        
        SalleTemplate template = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();

        type = 0;

        foreach (GameObject porte in portes)
        {
            int indice = porte.GetComponent<HeroCreationSalle>().ouverture ;
            switch (indice)
            { // [ H,D,B,G]
                case 1:
                    indice = 2;// demande ouverture pour haut donc ouverture par le bas  
                    break;
                case 2:
                    indice = 3; // demande ouverture par la gauche donc ouverture par la droite
                    break;
                case 3: 
                    indice = 0; // demande ouverture pour bas donc ouverture par le haut
                    break;
                case 4:
                    indice = 1; // demade ouverture par la droite donc ouverture par la gauche
                    break;
                default:
                    break;

            }
            signature[indice] = 1;

        }
        foreach(int porte in signature)
        {
            if(porte == 1)
            {
                type++;
            }
        }
        if ((signature[0] == 1 && signature[2] == 1 ) || (signature[1] == 1 && signature[3] == 1)){ // [1,0,1,0] [0,1,0,1]
            type++;

        }
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

    public int GetPorte(int indice)
    {
        return signature[indice];
    }
    public int getType()
    {
        return type - 1;
    }

    public void MseAjourCarte()
    {
        foreach (GameObject porte in portes)
        {
            int indice = porte.GetComponent<HeroCreationSalle>().ouverture;
            switch (indice)
            { // [ H,D,B,G]
                case 1:
                    indice = 2;// demande ouverture pour haut donc ouverture par le bas  
                    break;
                case 2:
                    indice = 3; // demande ouverture par la gauche donc ouverture par la droite
                    break;
                case 3:
                    indice = 0; // demande ouverture pour bas donc ouverture par le haut
                    break;
                case 4:
                    indice = 1; // demade ouverture par la droite donc ouverture par la gauche
                    break;
                default:
                    break;

            }
            signature[indice] = 1;

        }
        foreach (int porte in signature)
        {
            if (porte == 1)
            {
                type++;
            }
        }
        if ((signature[0] == 1 && signature[2] == 1) || (signature[1] == 1 && signature[3] == 1))
        { // [1,0,1,0] [0,1,0,1]
            type++;

        }
    }
    public int[] getSignature()
    {
        return signature;
    }

    public GameObject destructionPorte(int ouverture)
    {
        switch (ouverture)
        { // 
            case 1:
                ouverture = 3;// demande ouverture pour haut donc ouverture par le bas  
                break;
            case 2:
                ouverture = 4; // demande ouverture par la gauche donc ouverture par la droite
                break;
            case 3:
                ouverture = 1; // demande ouverture pour bas donc ouverture par le haut
                break;
            case 4:
                ouverture = 2; // demade ouverture par la droite donc ouverture par la gauche
                break;
            default:
                break;

        }
        GameObject PorteDestructon = null;
        bool trouve = false;
        int indice = 0;
        while(!trouve && indice < portes.Length)
        {
            if(portes[indice].GetComponent<HeroCreationSalle>().getOuverture() == ouverture)
            {
                trouve = true;
                PorteDestructon = portes[indice];

            }
            indice++;
        }
        PorteDestructon.GetComponent<BoxCollider2D>().enabled = false;
       // PorteDestructon.GetComponent<HeroCreationSalle>().porte.GetComponent<BoxCollider2D>().enabled = false;

        return PorteDestructon.GetComponent<HeroCreationSalle>().porte;
        //Destroy(PorteDestructon);
    }

}
