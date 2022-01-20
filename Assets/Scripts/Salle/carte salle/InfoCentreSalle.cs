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

    public GameObject Porte { get => porte; set => porte = value; }

    public bool posable;
    private SalleTemplate templates;

    void Start()
    {
       
        templates = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();
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
        posable = true;

        // Update is called once per frame
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("SpawnPoint") && this.CompareTag("SpawnPoint"))
        {
            bool mainPosable = transform.parent.gameObject.GetComponent<MainCentre>().getMainPosable();
            bool mainPosableAutre = otherObject.transform.parent.gameObject.GetComponent<MainCentre>().getMainPosable();
            if (salle == null && otherObject.GetComponent<InfoCentreSalle>().salle == null && mainPosable && mainPosableAutre)
            {
                Debug.Log(" les 2 centre : PAs de salles");
                // ON fait la somme des 2 centres qui va nous donner la signature de la salle qui sera obligatoire celle l� ! 
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
                        if (!(nombreOuverture == 2))
                        {
                            setPosable(false);
                            Debug.Log(porte.transform.parent.parent.gameObject.GetComponent<GeneratorCarte>().title + " la sallle ne peut pas etre pos�");

                        }

                        Debug.Log(" les 2 centre : PAs de salles => IMPOSSSIBLE DE CONSTRUIRE " + nouveauNbreOuverture);
                    }
                }



            }
            else if (salle != null && otherObject.GetComponent<InfoCentreSalle>().salle == null && mainPosable && mainPosableAutre)
            {
                Debug.Log("MOi j'ai une sallle mais pas le Other : " + salle.GetComponent<GeneratorCarte>().title);

                if (0 < nombreOuverture && nombreOuverture < 2 && !centres.Contains(otherObject.gameObject))
                {
                    int[] sommesPortes = new int[portes.Length];
                    for (int indice = 0; indice < portes.Length; indice++)
                    {

                        sommesPortes[indice] = (portes[indice] + otherObject.GetComponent<InfoCentreSalle>().GetPorte(indice)) % 2;
                    }

                    Debug.Log(" nouveau Somme Portes : " + sommesPortes[0] + sommesPortes[1] + sommesPortes[2] + sommesPortes[3]);
                    int nouveauNbreOuverture = verificationNombreOuverture(sommesPortes);

                    if (nouveauNbreOuverture == 0 || verificationAvecSalle(sommesPortes, salle.GetComponent<GeneratorCarte>().signature) == true)
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

        }

        else if (otherObject.CompareTag("SpawnPoint") && this.CompareTag("Test"))
        { // pour le teste de posable ou pas 
            
            bool mainPosable = transform.parent.gameObject.GetComponent<MainCentre>().getMainPosable();

            Debug.Log(" JE teste ! "  + mainPosable);
            if (mainPosable && otherObject.GetComponent<InfoCentreSalle>().nombreOuverture == 0 || otherObject.GetComponent<InfoCentreSalle>().nombreOuverture == 2)
            {
                posable = false;

               
                templates.supprimmersalle(transform.parent.parent.gameObject.GetComponent<GeneratorCarte>().title);
                Debug.Log(" Pas possible de mmetre cette salle " );
                Destroy(transform.parent.parent.gameObject);

                transform.parent.gameObject.GetComponent<MainCentre>().miseAjourPosable();
            }
            else if(mainPosable)
            {
                int[] sommesPortes = new int[portes.Length];
                for (int indice = 0; indice < portes.Length; indice++)
                {

                    sommesPortes[indice] = (portes[indice] + otherObject.GetComponent<InfoCentreSalle>().GetPorte(indice)) % 2;
                }
                int nouveauNbreOuverture = verificationNombreOuverture(sommesPortes);
                if (!(nouveauNbreOuverture == 0 ||(otherObject.GetComponent<InfoCentreSalle>().salle != null && verificationAvecSalle(sommesPortes, otherObject.GetComponent<InfoCentreSalle>().salle.GetComponent<GeneratorCarte>().signature) == true)))
                {
                    posable = false;
                    templates.supprimmersalle(transform.parent.parent.gameObject.GetComponent<GeneratorCarte>().title);
                    Destroy(transform.parent.parent.gameObject, 500f);
                    transform.parent.gameObject.GetComponent<MainCentre>().miseAjourPosable();

                }
            }
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
        //Debug.Log(" >>>>>>>>>>>>>>>>>>>Dans Info Centre Salle Tirage  TesTSalles  172 ");
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
                    Debug.Log("Dans Info Centre Salle Tirage  TesTSalles  nbre ouverture :" + testNbreOuverture + type[rotation]);
                    type[rotation].transform.GetChild(1).gameObject.GetComponent<MainCentre>().changerTagTest();
                    type[rotation].GetComponent<GeneratorCarte>().changerLayerTest();
                    GameObject carte = Instantiate(type[rotation], transform.position, type[rotation].transform.rotation);
                    Debug.Log("carte layer " + carte.layer);

               
                   
                        sallesBonnes.Add(type[rotation]);

                        templates.listsalleTest.Add(carte);
                    
                   
                    
                
                  

                   type[rotation].transform.GetChild(1).gameObject.GetComponent<MainCentre>().changerTagSpwan();
                    type[rotation].GetComponent<GeneratorCarte>().changerLayerSalle();


                }
               


            }
            clas++;
        }
        
        foreach( GameObject salle in sallesBonnes)
        {
            Debug.Log(salle.GetComponent<GeneratorCarte>().title + " "+ salle.transform.GetChild(1).gameObject.GetComponent<MainCentre>().getMainPosable());
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
