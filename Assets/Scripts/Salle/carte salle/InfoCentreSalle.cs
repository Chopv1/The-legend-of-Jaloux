using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCentreSalle : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject[][] salles; //{ salleUnePorte, salleDeuxPorteL, salleDeuxPorteI }
    public int nombreOuverture= 0;
    public int porteH;
    public int porteD;
    public int porteB;
    public int porteG;
    public int[] portes;
    public GameObject porte;
    public GameObject salle;
    static int touche;

    public List<GameObject> centres;
    void Start()
    {
        
        portes = new int[] { this.porteH, this.porteD, this.porteB, this.porteG };
      
        int indice = porte.GetComponent<HeroCreationSalle>().ouverture - 1;
        portes[indice] = 1;
        foreach (int porte in portes)
        {
            if (porte == 1)
            {
                nombreOuverture += 1;
            }
        }

        salles = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>().tabSalles;
        centres.Add(transform.gameObject);
        // Update is called once per frame
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("SpawnPoint"))
        {
            touche += 1;
           // Debug.Log(" resultat centre touché " + touche);
            if (nombreOuverture < 2 &&   !centres.Contains( otherObject.gameObject)  )
            {
                int[] sommesPortes = new int[portes.Length];
                centres.Add(otherObject.gameObject);
                otherObject.GetComponent<InfoCentreSalle>().AjoutCentre(transform.gameObject);
            
                for (int indice=0;indice < portes.Length; indice++)
                {
                   
                    sommesPortes[indice] = (portes[indice] + otherObject.GetComponent<InfoCentreSalle>().GetPorte(indice)) % 2;
                }

                int nouveauNbrOUverture =verificationNombreOuverture(sommesPortes);

                if (nouveauNbrOUverture <= 2)
                {
                   // Debug.Log(" ancien Portes : " + portes[0] + portes[1] + portes[2] + portes[3]);
                   // Debug.Log(" l'autre portes : " + otherObject.GetComponent<InfoCentreSalle>().GetPorte(0) + otherObject.GetComponent<InfoCentreSalle>().GetPorte(1) + +otherObject.GetComponent<InfoCentreSalle>().GetPorte(2) + otherObject.GetComponent<InfoCentreSalle>().GetPorte(3));
                  //  Debug.Log(" nouveau Portes : " + sommesPortes[0]+ sommesPortes[1] +sommesPortes[2]+ sommesPortes[3]);
                    for (int indice = 0; indice < portes.Length; indice++)
                    {

                        portes[indice] = sommesPortes[indice];
                        otherObject.GetComponent<InfoCentreSalle>().SetPorte(sommesPortes[indice], indice);


                    }

                    nombreOuverture = nouveauNbrOUverture;
                    otherObject.GetComponent<InfoCentreSalle>().MiseAjourNombreouverture();
                    //Destroy(otherObject);
                    //Debug.Log(" nouveau centre  destruction ! " );

                   // Debug.Log("  nouveau Nombre OUverture : " + nombreOuverture);
                }
                else
                {
                    //Destroy(transform.parent.parent.parent.gameObject );
                    //Debug.Log(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!        destruction  nouveau Nombre OUvertu !!!!!!!!!!!!!!!!!! " + nombreOuverture);
                    // enlever de la liste des centres ! 
                    centres.Remove(otherObject.gameObject);
                    otherObject.GetComponent<InfoCentreSalle>().centres.Remove(transform.gameObject);
                  // MiseAjourCentre();

                }

            }
            else if (nombreOuverture ==2 && !centres.Contains(otherObject.gameObject))
            {
              //  Debug.Log("  !!!!!!!!!!!!!!!!!!! impossible de construire ");
                otherObject.GetComponent<InfoCentreSalle>().DestructionSalle();
                
                

            }
        }
    }


    public int GetPorte(int indice)
    {
        return portes[indice];
    }
    public void SetPorte(int value, int indice)
    {
        portes[indice] = value;
    }
    public void AjoutCentre(GameObject centre)
    {
        centres.Add(centre);
    }
    public void MiseAjourNombreouverture()
    {
        nombreOuverture = 0;
        foreach (int porte in portes)
        {
            if (porte == 1)
            {
                nombreOuverture += 1;
            }
        }

    }
    public int indice()
    {
        int i = 0;
        while(portes[i] != 1)
        {
            i++;
        }

        return i;
    }
    public List<GameObject>  GetCentre()
    {
        return centres;
    }
    public void MiseAjourCentre()
    {

        int[] signatureSalle = salle.GetComponent<GeneratorCarte>().getSignature();

        for(int indice =0; indice<4; indice++)
        {
            portes[indice] = (portes[indice] + signatureSalle[indice]) % 2;
        }

        /*
        centres.RemoveAll(GameObject => GameObject == null);
        for (var i = centres.Count - 1; i > -1; i--)
        {
            if (centres[i] == null)
                centres.RemoveAt(i);
        } */
        
      /*  List<GameObject> sallesDestrcution= new List<GameObject>();
        foreach(GameObject salle in centres)
        {
            if ( !salle.CompareTag ("SpawnPoint"))
            {
                Debug.Log("  !!!!!!!!!!!!!!!!!!! salle de construire ");
                salle = GameObject gameObject; 
                sallesDestrcution.Add(salle);
            }
        }
        foreach(GameObject salleVide in sallesDestrcution)
        {
            centres.Remove(salleVide);
        }
     */   
    }
    public List<GameObject> TesTSalles(List<GameObject[]> sallePotentiel)
    {
        Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle Tirage  TesTSalles  172 ");
        Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle taille taille sallePotentiek " + sallePotentiel.Count);
        List<GameObject> sallesBonnes = new List<GameObject>();
        int clas = 0;
        foreach (GameObject[] type in sallePotentiel)
        {
            Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle taille taille sallePotentiel type " + clas +" taille de type " + type.Length);
            int[] sommesPortes = new int[portes.Length];
            for (int rotation = 0; rotation < type.Length; rotation++)
            {
                Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle taille taille sallePotentiel type " + clas + " nombre roation "+ rotation );
                for (int indice =0; indice < portes.Length; indice++)
                {
                    sommesPortes[indice] = portes[indice] + type[rotation].GetComponent<GeneratorCarte>().GetPorte(indice);
                }
                int testNbreOuverture =verificationNombreOuverture(sommesPortes);
                Debug.Log("Dans Info Centre Salle Tirage  TesTSalles  nbre ouverture :" + testNbreOuverture);
                if ( testNbreOuverture < 2)
                {
                    GameObject carte = Instantiate(type[rotation], transform.position, type[rotation].transform.rotation);
                    Destroy(carte);
                        sallesBonnes.Add(type[rotation]);
                    
                }
               


            }
            clas++;
        }
        Debug.Log("Dans Info Centre Salle Tirage  TesTSalles  172 :" + sallesBonnes.Count);
        return sallesBonnes;

    }
    public void DestructionSalle()
    {
        Destroy(salle);
    }

    public int verificationNombreOuverture(int [] portesVerifcatuion)
    {
        int nombreOuvertureVerification = 0;
        foreach (int porte in portesVerifcatuion)
        {
            if (porte == 1)
            {
                nombreOuvertureVerification += 1;
            }
        }
        return nombreOuvertureVerification;
    }
    public void afficheSignature()
    {
        string signature = "";
        foreach(int valeur in portes)
        {
            signature += valeur;
        }
        Debug.Log("siganture : " + signature);
    }


  
}
