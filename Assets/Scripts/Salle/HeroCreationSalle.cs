using System.Collections.Generic;
using UnityEngine;

public class HeroCreationSalle : MonoBehaviour
{
    public int ouverture;
    private SalleTemplate templates;
    private int rand; // pour aléatoire
    private List<GameObject> salles;
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
                Tirage();

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


            }

        }
        else if (otherObject.CompareTag("Porte"))
        {
            //Destroy(otherObject);
            Destroy(this);
            Debug.Log("nop porte");
        }
    }

    public void Tirage()
    {/*
		Debug.Log("carte debut" + " "+ templates.cartes.Length + " "+ " " + templates.salles.Count);
		info = templates.salles[1].GetComponent<GeneratorCarte>();
		info.setDescription("ouias oauis");
		templates.salles[1].GetComponent<GeneratorCarte>().setDescription("lourd");


		GameObject.FindGameObjectWithTag("Carte1").GetComponent<ShopPanel>().salle.title = "ouis ";

		*/
        /* tirage de salle*/
        // changer de camera vue des cartes
        // GameObject.FindGameObjectWithTag("Camera").GetComponent<GestionCamera>().changerCameraCarte();
        GameObject.FindGameObjectWithTag("Camera").GetComponent<GestionCamera>().changerCarte();
        int nombreC = 0;
        Debug.Log("carte debut" + templates.cartes.Length + " " + templates.salleHaut.Length + " " + templates.salles.Count);
        for (int indice = 0; indice < templates.cartes.Length; indice++)
        {

            rand = Random.Range(0, templates.salles.Count - 1);

            /* recherche de la position de la carte puis envoyer les information de la salle choisi pour changer les info de carte ne visuelle */

            GameObject.FindGameObjectWithTag("Carte" + (indice + 1)).GetComponent<ShopPanel>().ChangerCarte(templates.salles[rand].GetComponent<GeneratorCarte>());
            /*carte[indice].GetComponent<ShopPanel>().ChangerCarte(info); */

            nombreC += 1;
        }

    }
    

}


