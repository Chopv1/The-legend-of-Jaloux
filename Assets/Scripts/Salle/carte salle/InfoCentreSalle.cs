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
    bool posable = true;
 
    void Start()
    {
        
        portes = new int[] { this.porteH, this.porteD, this.porteB, this.porteG };
        nombreOuverture = 1;
      
        int indice = porte.GetComponent<HeroCreationSalle>().ouverture - 1;
        portes[indice] = 1;
      /*
        foreach (int porte in portes)
        {
            if (porte == 1)
            {
                nombreOuverture += 1;
            }
        }
         */
        salles = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>().tabSalles;

       if(!centres.Contains(transform.gameObject) && transform.gameObject !=null)
        {
            centres.Add(transform.gameObject);
        }

        // Update is called once per frame
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("SpawnPoint"))
        {
            bool mainPosable = transform.parent.gameObject.GetComponent<MainCentre>().getMainPosable();
            bool mainPosableAutre = otherObject.transform.parent.gameObject.GetComponent<MainCentre>().getMainPosable();
            if (salle == null && otherObject.GetComponent<InfoCentreSalle>().salle == null && mainPosable && mainPosableAutre)
            {
                Debug.Log(" les 2 centre : PAs de salles");
                // ON fait la somme des 2 centres qui va nous donner la signature de la salle qui sera obligatoire celle là ! 
                int[] sommesPortes = new int[portes.Length];
                if (0 < nombreOuverture && nombreOuverture < 2 && !centres.Contains(otherObject.gameObject))
                {
                    for (int indice = 0; indice < portes.Length; indice++)
                    {

                        sommesPortes[indice] = (portes[indice] + otherObject.GetComponent<InfoCentreSalle>().GetPorte(indice)) % 2;
                    }
                    int nouveauNbreOuverture = verificationNombreOuverture(sommesPortes);

                    if (nouveauNbreOuverture == 2)
                    {
                        for (int indice = 0; indice < portes.Length; indice++)
                        {

                            portes[indice] = sommesPortes[indice];
                            otherObject.GetComponent<InfoCentreSalle>().SetPorte(portes[indice], indice);
                        }
                        MiseAjourNombreouverture();
                        otherObject.GetComponent<InfoCentreSalle>().MiseAjourNombreouverture();
                        centres.Add(otherObject.gameObject);
                        otherObject.GetComponent<InfoCentreSalle>().AjoutCentre(transform.gameObject);
                    }
                    else
                    {
                        if( !(nombreOuverture == 2)) {
                            setPosable(false);
                            Debug.Log( porte.transform.parent.parent.gameObject.GetComponent<GeneratorCarte>().title +" la sallle ne peut pas etre posé");

                        }
                        
                        Debug.Log(" les 2 centre : PAs de salles => IMPOSSSIBLE DE CONSTRUIRE " + nouveauNbreOuverture);
                    }
                }
                   


            }
            else if (salle != null && otherObject.GetComponent<InfoCentreSalle>().salle == null && mainPosable && mainPosableAutre)
            {
                Debug.Log("MOi j'ai une sallle mais pas le Other : " + salle.GetComponent<GeneratorCarte>().title);
                
                if (0<nombreOuverture && nombreOuverture < 2 && !centres.Contains(otherObject.gameObject))
                {
                    int[] sommesPortes = new int[portes.Length];
                    for (int indice = 0; indice < portes.Length; indice++)
                    {

                        sommesPortes[indice] = (portes[indice] + otherObject.GetComponent<InfoCentreSalle>().GetPorte(indice))%2 ;
                    }

                    Debug.Log(" nouveau Somme Portes : " + sommesPortes[0] + sommesPortes[1] + sommesPortes[2] + sommesPortes[3]);
                    int nouveauNbreOuverture = verificationNombreOuverture(sommesPortes);
                    
                    if (nouveauNbreOuverture  == 0 || verificationAvecSalle(sommesPortes,salle.GetComponent<GeneratorCarte>().signature) == true)
                    {
                        for (int indice = 0; indice < portes.Length; indice++)
                        {

                            portes[indice] = sommesPortes[indice];
                            otherObject.GetComponent<InfoCentreSalle>().SetPorte(portes[indice], indice);
                        }

                        MiseAjourNombreouverture();
                        otherObject.GetComponent<InfoCentreSalle>().MiseAjourNombreouverture();
                        centres.Add(otherObject.gameObject);
                        otherObject.GetComponent<InfoCentreSalle>().AjoutCentre(transform.gameObject);
                        otherObject.GetComponent<InfoCentreSalle>().setSalle(salle);
                        setSalle(salle);


                    }
                    else
                    {
                        Debug.Log("Construction IMPOSSIBLE apres sommes");
                    }

                }
                else
                {
                    Debug.Log("Construction IMPOSSIBLE");
                }
            }
            /*repetition 
            else if (salle == null && otherObject.GetComponent<InfoCentreSalle>().salle != null)
            {
                Debug.Log("MOi j'ai Pas de sallle maiss le Other Si ! " );
            }
            */
            /*
             * 
             */

                /*
                MiseAjourNombreouverture();
                Debug.Log("ma salle est vide ?" + salle != null);
                touche += 1;
                // Debug.Log(" resultat centre touché " + touche);
                if (nombreOuverture < 2 && !centres.Contains(otherObject.gameObject))
                {
                    int[] sommesPortes = new int[portes.Length];
                    centres.Add(otherObject.gameObject);
                    otherObject.GetComponent<InfoCentreSalle>().AjoutCentre(transform.gameObject);
                    if (salle != null) {
                        setSalleCentre(salle);
                        otherObject.GetComponent<InfoCentreSalle>().centres = centres;
                        otherObject.GetComponent<InfoCentreSalle>().setSalle
                    }



                    if (salle != null)
                    {
                        Debug.Log("Salle ajouter");
                        otherObject.GetComponent<InfoCentreSalle>().salle = this.salle;
                    } 

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

                            MiseAjourCentre();    
                            otherObject.GetComponent<InfoCentreSalle>().MiseAjourCentre();



                        //Destroy(otherObject);
                        //Debug.Log(" nouveau centre  destruction ! " );

                       // Debug.Log("  nouveau Nombre OUverture : " + nombreOuverture);
                    }
                    else
                    {
                        //Destroy(transform.parent.parent.parent.gameObject );
                        Debug.Log(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!        destruction  nouveau Nombre OUvertu !!!!!!!!!!!!!!!!!! " + nombreOuverture);
                        // enlever de la liste des centres ! 
                        centres.Remove(otherObject.gameObject);
                        otherObject.GetComponent<InfoCentreSalle>().centres.Remove(transform.gameObject);
                      // MiseAjourCentre();

                    }

                }
                else if (nombreOuverture ==2 && !centres.Contains(otherObject.gameObject))
                {/*
                    int nombreOuvertureVerification = verificationNombreOuverture(portes);
                    if (nombreOuverture == 0)
                    {
                        Debug.Log(" !!!!!!§§§§§§§C'est on construit");
                        MiseAjourCentre();
                    }
                    else
                    {
                        Debug.Log("  !!!!!!!!!!!!!!!!!!! impossible de construire ");
                        otherObject.GetComponent<InfoCentreSalle>().DestructionSalle();
                    }
                  */





        }
    }

    public void setPosable(bool posable)
    {
        this.posable = posable;
    }
    public bool getPosable()
    {
        return posable;
    }
    public bool verificationAvecSalle(int[] signatureCentre, int[] signatureSalle)
    {
        bool construction = true;
        int ouverture = 0;
        while ( ouverture < signatureCentre.Length && construction)
        {
            if (signatureCentre[ouverture] != signatureSalle[ouverture])
            {
                Debug.Log(" VErif Avec sAlle " + ouverture + "centee " + signatureCentre[ouverture] + " salle " +signatureSalle[ouverture]);
                construction = false;
            }
            ouverture++;
        }
        return construction;

    }
    public int GetPorte(int indice)
    {
        return portes[indice];
    }
    public int[] getSignature()
    {
        return portes;
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

        if (salle != null && nombreOuverture !=0)
        {
           
            int[] signatureSalle = salle.GetComponent<GeneratorCarte>().getSignature();
            Debug.Log(" signatureSalle : " + signatureSalle[0] + signatureSalle[1] + signatureSalle[2] + signatureSalle[3]);
            Debug.Log(" portes : " + portes[0] + portes[1] + portes[2] + portes[3]);
            for (int indice = 0; indice < 4; indice++)
            {
                portes[indice] = (portes[indice] + signatureSalle[indice]) % 2;
            }
        }
        MiseAjourNombreouverture();


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
    public void MiseAjourSalle()
    {
        salle = porte.transform.transform.gameObject;
    }
    public List<GameObject> TesTSalles(List<GameObject[]> sallePotentiel)
    {
       // Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle Tirage  TesTSalles  172 ");
        //Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle taille taille sallePotentiek " + sallePotentiel.Count);
        List<GameObject> sallesBonnes = new List<GameObject>();
        int clas = 0;
        foreach (GameObject[] type in sallePotentiel)
        {
          //  Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle taille taille sallePotentiel type " + clas +" taille de type " + type.Length);
            int[] sommesPortes = new int[portes.Length];
            for (int rotation = 0; rotation < type.Length; rotation++)
            {
                //Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle taille taille sallePotentiel type " + clas + " nombre roation "+ rotation );
                for (int indice =0; indice < portes.Length; indice++)
                {
                    sommesPortes[indice] = portes[indice] + type[rotation].GetComponent<GeneratorCarte>().GetPorte(indice);
                }
                int testNbreOuverture =verificationNombreOuverture(sommesPortes);
                //Debug.Log("Dans Info Centre Salle Tirage  TesTSalles  nbre ouverture :" + testNbreOuverture);
                if ( testNbreOuverture < 2)
                {
                    GameObject carte = Instantiate(type[rotation], transform.position, type[rotation].transform.rotation);
                    Destroy(carte);
                        sallesBonnes.Add(type[rotation]);
                    
                }
               


            }
            clas++;
        }
        //Debug.Log("Dans Info Centre Salle Tirage  TesTSalles  172 :" + sallesBonnes.Count);
        return sallesBonnes;

    }
    public void DestructionSalle()
    {
        Destroy(salle);
    }

    public int verificationNombreOuverture(int [] portesVerificatuion)
    {
        int nombreOuvertureVerification = 0;
        foreach (int porte in portesVerificatuion)
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

    public void setSalleCentre(GameObject salle) {
        int nombreCentre = 0 ;
        this.salle = salle;
        MiseAjourCentre();
        foreach (GameObject centre in centres)
        {
            Debug.Log(" Ajouter salle " + salle.GetComponent<GeneratorCarte>().title + "centre " +nombreCentre);
            centre.GetComponent<InfoCentreSalle>().setSalle(salle);
            centre.GetComponent<InfoCentreSalle>().MiseAjourCentre();
           nombreCentre++;
        }
       
    }

    public void setSalle(GameObject salleAjouter)
    {
        Debug.Log(" >> Maintenant jai la salle : " + salleAjouter.GetComponent<GeneratorCarte>().title);

        salle = salleAjouter;
        MiseAjourCentre();
        
    }
  
}
