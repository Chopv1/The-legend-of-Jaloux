using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public TMP_Text pointActionTxt;
    public GameObject salleObject;
    public int rotation;
    public GameObject[] salleTypeRoatation;
    public SalleInfo salle;
    public bool changer;

    private int rand;
    private SalleTemplate templates;
    public GameObject centre;
    public GameObject porte;
    bool posable = false;

    public GameObject generationMap;
    private void Start()
    {
        titleTxt.text = salle.title;
        descriptionTxt.text = salle.description;
        pointActionTxt.text = salle.pointAction.ToString();
        templates = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();
        changer = false;

    }

    private void Update()
    {

        if (changer == true)
        {
            this.titleTxt.text = titleTxt.text;
            this.descriptionTxt.text = descriptionTxt.text;
            this.pointActionTxt.text = pointActionTxt.text;
            

            changer = false;
            Debug.Log(" resultat changement" + titleTxt.text);
        }

    }

    public void ChangerCarte(GeneratorCarte carte, GameObject salle, GameObject centre, GameObject[] salleRotation,int rotationSalle,GameObject porte)
    {
        titleTxt.text = carte.title;
        descriptionTxt.text = carte.description;
        pointActionTxt.text = carte.pointAction.ToString();
        salleObject = salle;
        this.centre = centre;
        this.porte = porte;
        changer = true;
        salleTypeRoatation = salleRotation;
        rotation = rotationSalle;
        Update();
        changer = false;
        


    }

    public void ChoisirSalle()
    {
        posable = templates.sallePosable(salleObject);
        templates.destructionSalleTest();

        if (!posable)
        {
            Debug.Log(" >>>>>>> !!!!!!!!!!! Impossible de poser Astuce faire une rotation ");
           // button.SetActive(false);
        }
        else
        {
          //  button.SetActive(true);
            Debug.Log(" 33333333333333   C'est bon ! ");
            GameObject carte = Instantiate(salleObject, centre.transform.position, salleObject.transform.rotation);
            centre.GetComponent<InfoCentreSalle>().setSalleCentre( carte);
            centre.GetComponent<InfoCentreSalle>().MiseAjourCentre();
            if (templates.getListeSallesBonnes().Count == 1)
            {
                carte.transform.GetChild(1).GetComponent<MainCentre>().MiseAjourCentreSignature();
            }
          
          // centre..GetComponent<InfoCentreSalle>().MiseAjourCentre();

           //centre.GetComponent<VericationConstruction>();

            GameObject ouverture = carte.GetComponent<GeneratorCarte>().destructionPorte(porte.GetComponent<HeroCreationSalle>().getOuverture());

            centre.GetComponent<VericationConstruction>();
            carte.GetComponent<GeneratorCarte>().destructionPorte(porte.GetComponent<HeroCreationSalle>().getOuverture());
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GestionCamera>().changerMap();
            generationMap.GetComponent<TileMap>().GenerationSalle(centre);
            generationMap.GetComponent<TileMap>().TPhero(carte, porte.GetComponent<HeroCreationSalle>().getOuverture());
            generationMap.GetComponent<TileMap>().nom = carte.GetComponent<GeneratorCarte>().title;
        }
      
       // Rotate(carte);
        
        /*
        if (ouverture == 1)
        {s
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
        */
        
    }

   
    public void Rotate()
    {
        
        Debug.Log(" ROtation Actuel  : " + rotation) ;
        if(rotation+1 < salleTypeRoatation.Length)
        {
            rotation += 1;
            salleObject = salleTypeRoatation[rotation ];
            
            Debug.Log(" >> ROtation Apres  : " + rotation);
          


        }
        else
        {
            rotation = 0;
            salleObject = salleTypeRoatation[rotation];
            Debug.Log(" > ROtation Actuel  : " + rotation);
        }

        titleTxt.text = salleObject.GetComponent<GeneratorCarte>().title;
      
       
    }

    public void formeSalle()
    {
        
    }



}
