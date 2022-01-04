using System.Collections.Generic;
using UnityEngine;

public class HeroCreationSalle : MonoBehaviour
{
    public int ouverture;
    public GameObject porte;
    private SalleTemplate templates;
    private int rand; // pour aléatoire
    public List<GameObject> salles;
    private GameObject[] carte;
    private GeneratorCarte info;
    private SalleInfo salleInfo;


    public bool construction = false;
    Collider2D other;
    public Vector2 forceSaut = new Vector2(0, 500);

    public float waitTime = 4f;

    // Start is called before the first frame update
    void Start()

    {

        templates = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();


    }

    // Update is called once per frame
    void Update()
    {



    }
    // Lorsque le collider entre en contact avec quelque chose
    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Hero"))
        { // déclanchement au contacte d'un hero 

            if (construction == false)
            {
                Tirage(porte);

                /*
                if (ouverture == 1)
                {
                    // une porte en haut 
                    rand = Random.Range(0, templates.salleHaut.Length);
                    Instantiate(templates.salleHaut[rand], transform.position + new Vector3(0, -6), templates.salleHaut[rand].transform.rotation);
                    transform.Translate(Vector2.down * 60f * Time.deltaTime);

                }
                else if (ouverture == 2)
                {
                    // pour une porte à droite
                    rand = Random.Range(0, templates.salleDroite.Length);
                    Instantiate(templates.salleDroite[rand], transform.position + new Vector3(-6, 0), templates.salleDroite[rand].transform.rotation);
                    transform.Translate(Vector2.left * 60f * Time.deltaTime);

                }
                else if (ouverture == 3)
                {
                    //  3 pour une porte à bas
                    rand = Random.Range(0, templates.salleBas.Length);
                    Instantiate(templates.salleBas[rand], transform.position + new Vector3(0, 6), templates.salleBas[rand].transform.rotation);
                    transform.Translate(Vector2.up * 60f * Time.deltaTime);
                }
                else if (ouverture == 4)
                {
                    //  4 pour une porte à gauche
                    rand = Random.Range(0, templates.salleGauche.Length);
                    Instantiate(templates.salleGauche[rand], transform.position + new Vector3(6, 0), templates.salleGauche[rand].transform.rotation);
                    transform.Translate(Vector2.right * 60f * Time.deltaTime);
                }
                construction = true;

                */
                construction = true;
            }

        }
        else if (otherObject.CompareTag("Porte"))
        {
            //Destroy(otherObject);
            Destroy(this);
            Debug.Log("nop porte");
        }
    }

    public void Tirage(GameObject ouverture)
    {
        GameObject.FindGameObjectWithTag("Camera").GetComponent<GestionCamera>().changerCarte();
        // carte qui aura forcement une salle bonne
        int indiceCarteBonne = Random.Range(0, templates.cartes.Length - 1);


        for (int indice = 0; indice < templates.cartes.Length ; indice++)
        {
            Debug.Log(">>>> " + indice + " indice Bonne"+ indiceCarteBonne);
            if (indice == indiceCarteBonne)
            {
                rechercheSalle(ouverture, indice);

                
            }
            else
            {
                int typeSalle = Random.Range(0, templates.tabSalles.Length - 1);
                GameObject[] salleRoataion = templates.getSalleRotation(typeSalle);
                int rotationSalle = Random.Range(0, salleRoataion.Length - 1);
                GameObject salleCarte = templates.getSalle(typeSalle, rotationSalle);
                /* recherche de la position de la carte puis envoyer les information de la salle choisi pour changer les info de carte ne visuelle */
                GameObject.FindGameObjectWithTag("Carte" + (indice + 1)).GetComponent<ShopPanel>().ChangerCarte(salleCarte.GetComponent<GeneratorCarte>(), salleCarte, ouverture, salleRoataion, rotationSalle, this.gameObject);

              
            }
            

        }


         


           
        

    }

    public void rechercheSalle(GameObject ouverture, int indice)
    {
       
        int nombreOuverture = ouverture.GetComponent<InfoCentreSalle>().nombreOuverture;
        Debug.Log("Dans rechercheSalle 138 ");


        if (nombreOuverture == 1)
        {
            int[] portesCentre = ouverture.GetComponent<InfoCentreSalle>().portes;
            int[] tipeSalles = new int[3];
            int indiceUN = ouverture.GetComponent<InfoCentreSalle>().indice();
            GameObject[][] tabSalles = templates.tabSalles;
            int typesalle = 0;
            List<GameObject[]> sallesPotentiels = new List<GameObject[]>();
            // Debug.Log("Dans Hero CReation Tirage taille tabSalles nombre de types " + tabSalles.Length);
           
            for (int type = 0; type < tabSalles.Length; type++) // deplacement par type : une porte , en L , en I 
            {
               
                bool trouver = false;
                int indiceSalle = 0;
                int nombreSalle = templates.tabSalles[type].Length;

                //Debug.Log("Dans Hero CReation Tirage nombre salles type " + type + " : " + nombreSalle);
                while (!trouver && indiceSalle < nombreSalle)
                {
                    int[] signature = tabSalles[type][indiceSalle].GetComponent<GeneratorCarte>().signature;
                   // Debug.Log("salle "+  typesalle + " indice siganture : " + indiceUN  +" = "+ signature[0] + signature[1] + signature[2] + signature[3]);
                    if (signature[indiceUN] == 1)
                    {
                      
                        trouver = true;
                        sallesPotentiels.Add(tabSalles[type]);
                        tipeSalles[typesalle] = type;
                        typesalle++;
                    }
                    indiceSalle++;

                }

            }
            
            int typeC = 0;

            Debug.Log("Dans rechercheSalle ds  for  sallePOtentiel " + sallesPotentiels.Count);
            foreach (GameObject[] Typesalle in sallesPotentiels)
            {
               
                int rotation = 0;
                foreach (GameObject salle in Typesalle)
                {
                    int[] signature = tabSalles[typeC][rotation].GetComponent<GeneratorCarte>().signature;
                   // Debug.Log("Type salle " + typeC + " roataione : " + rotation + " = " + signature[0] + signature[1] + signature[2] + signature[3]);
                    rotation++;
                }
                typeC++;
            }
           
            // choix de la salle aleatoire

            int typeSalle = Random.Range(0, sallesPotentiels.Count - 1);
            int rotationSalle = Random.Range(0, sallesPotentiels[typeSalle].Length - 1);
            // Debug.Log("Hero" + sallesPotentiels[0][0].GetComponent<GeneratorCarte>().getType() + templates.getSalle(0, 0).GetComponent<GeneratorCarte>().type);
            
            Debug.Log("Dans rechercheSalle choix de la salle aleatoire  " + typeSalle + " rotationSalle " + rotationSalle + " tipeSalles " + tipeSalles[typeSalle]);
            GameObject[] salleRotation = templates.getSalleRotation(tipeSalles[typeSalle]);

           
            // creation de la carte 
            
            GameObject.FindGameObjectWithTag("Carte" + (indice + 1)).GetComponent<ShopPanel>().ChangerCarte(sallesPotentiels[typeSalle][rotationSalle].GetComponent<GeneratorCarte>(), sallesPotentiels[typeSalle][rotationSalle], ouverture, salleRotation, rotationSalle, this.gameObject) ;
           
            salles = ouverture.GetComponent<InfoCentreSalle>().TesTSalles(sallesPotentiels);// List<GameObject> la liste des salles bonnes rotation
            templates.setListeSallesBonnes(salles);
        }
    }

    public void setPorte(GameObject porte)
    {
        this.porte = porte;
    }

    public int getOuverture()
    {
        return ouverture;
    }

}


