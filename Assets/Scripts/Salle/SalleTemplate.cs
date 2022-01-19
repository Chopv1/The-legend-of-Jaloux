using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalleTemplate : MonoBehaviour
{
	public GameObject[] salleBas;
	public GameObject[] salleHaut;
	public GameObject[] salleGauche;
	public GameObject[] salleDroite;
	public GameObject[] cartes;


	public GameObject sallefermer;

	public List<GameObject> salles; // liste de toutes les salles

	public float tempsAttente;
	private bool spawnedBoss;
	public GameObject boss;
	public GameObject[] salleUnePorte;
	public GameObject[] salleDeuxPorteL;
	public GameObject[] salleDeuxPorteI;
	public GameObject[][] tabSalles;
	public List<List<int[]>> tabSignature ;


	public List<GameObject> ListSalleBonnes;
	public List<GameObject> listsalleTest;
	public GameObject salleActuel;

	private void Start()
    {
		tabSalles = new GameObject[][] { salleUnePorte, salleDeuxPorteL, salleDeuxPorteI };
		tabSignature = new List<List<int[]>>();



	}


    void Update()
	{
		/*
		if (tempsAttente <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < 4 ; i++)
			{
				if (i == salles.Count - 1)
				{
					Instantiate(boss, salles[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}
			}
		}
		else
		{
			tempsAttente -= Time.deltaTime;
		}
*/

	}
	public void enregistrementSignature()
    {
		
		
		int classe = 1;
		foreach (GameObject[] type in tabSalles)
		{
			//Debug.Log(" taille type " + type.Length);
			List<int[]> listetype = new List<int[]>();
			for (int rotation = 0; rotation < type.Length; rotation++)
			{
				GameObject salle = Instantiate(type[rotation], transform.position, type[rotation].transform.rotation);
				GeneratorCarte infosalle = salle.GetComponent<GeneratorCarte>();
				int[] signature = salle.GetComponent<GeneratorCarte>().signature;
				
				//Debug.Log("nom salle " + salle.GetComponent<GeneratorCarte>().title + " type " + salle.GetComponent<GeneratorCarte>().type + " type scrpt " + infosalle.type + " : " + signature[0] + signature[1] + signature[2] + signature[3]);
				
				salle.GetComponent<GeneratorCarte>().MseAjourCarte();
				//Debug.Log(" Apres Mise ajour >>>>>>>>> nom salle " + salle.GetComponent<GeneratorCarte>().title + " type " + salle.GetComponent<GeneratorCarte>().type + " type scrpt " + infosalle.type + " : " + signature[0] + signature[1] + signature[2] + signature[3]);
				Destroy(salle);
				listetype.Add(signature);
				//Destroy(salle);

			}
			/*
			Debug.Log("TYPE AVant ADD " +classe);

			foreach (int[] signature in listetype)
            {

				Debug.Log("EL SIGANTURE " +signature[0] + signature[1] + signature[2] + signature[3]);
            }*/
			
			tabSignature.Add(listetype);
			/*
			Debug.Log(">> NOMBRE TYPE " + tabSignature.Count);

			int typeS = 0;
			foreach (List<int[]> listeSignature in tabSignature)
			{
				Debug.Log(">>  TYPE " + typeS);
				int rotation = 0;
				Debug.Log(">>  Nombre rotation " + listeSignature.Count);
				foreach (int[] signature in listeSignature)
				{
					Debug.Log("type " + typeS + ">> EL SIGANTURE rotation " + rotation + " =" + signature[0] + signature[1] + signature[2] + signature[3]);
					rotation++;
				}
				typeS++;

			}
			*/
			

			


		}
		classe = 0;
		/*
		
			Debug.Log(">> NOMBRE TYPE " + tabSignature.Count);

			foreach(List<int[]> listeSignature in tabSignature)
        {
			Debug.Log(">>  TYPE " + classe );
			int rotation = 0;

			foreach (int[] signature in listeSignature)
            {
				Debug.Log("type " + classe + ">> EL SIGANTURE rotation " + rotation + " =" + signature[0] + signature[1] + signature[2] + signature[3]);
				rotation++;
			}
			classe++;

        }
		*/

		/*
			for( int type = 0; type< tabSignature.Count; type++)
			{
				Debug.Log(">> NOMBRE rotation " + tabSignature[type].Count);
			for ( int rotation = 0; rotation < tabSignature[type].Count; rotation++)
            {
				Debug.Log("type "+ type+ ">> EL SIGANTURE rotation " + rotation + " =" + tabSignature[type][0][0] + tabSignature[type][1] + tabSignature[type][2] + tabSignature[type][3]);
			}
				
				
			}
			*/
		getType();


	}
	public GameObject[] getSalleRotation(int indice)
    {
		
		return tabSalles[indice];
    }
	public void getType()
	{
		int typeS = 0;
		foreach (List<int[]> listeSignature in tabSignature)
		{
			Debug.Log(">>  TYPE " + typeS);
			int rotation = 0;
			Debug.Log(">>  Nombre rotation " + listeSignature.Count);
			foreach (int[] signature in listeSignature)
			{
				Debug.Log("type " + typeS + ">> EL SIGANTURE rotation " + rotation + " =" + signature[0] + signature[1] + signature[2] + signature[3]);
				rotation++;
			}
			typeS++;

		}
	}
	public GameObject getSalle(int type , int rotation)
    {
		
		return tabSalles[type][rotation];
    }

	public void setListeSallesBonnes(List<GameObject> salleBonnesTest)
	{
		List<GameObject> salleBonnes = new List<GameObject>();

		foreach (GameObject salle in salleBonnesTest)
		{
			Debug.Log(" >< salle template " + salle.GetComponent<GeneratorCarte>().title + " est posable ?" + salle.transform.GetChild(1).gameObject.GetComponent<MainCentre>().getMainPosable());
			if (salle.transform.GetChild(1).gameObject.GetComponent<MainCentre>().getMainPosable() == true)
			{
				salleBonnes.Add(salle);
			}
		}
		ListSalleBonnes = salleBonnes;
	}
	public List<GameObject> getListeSallesBonnes()
    {
	
		return ListSalleBonnes;

	}

	public bool sallePosable(GameObject salle)
    {
		bool posable = false;
		int taille = ListSalleBonnes.Count;
		int position = 0;
		while( position < taille && !posable)
        {
			if(ListSalleBonnes[position].GetComponent<GeneratorCarte>().title == salle.GetComponent<GeneratorCarte>().title)
            {
				posable = true;
            }
			position++;
        }

		return posable;

    }

	public void supprimmersalle(string nom)
    {
		int position = 0;
		bool suppimer = false;
		int taille = ListSalleBonnes.Count;
		while( position< ListSalleBonnes.Count && !suppimer)
        {
			if(ListSalleBonnes[position].GetComponent<GeneratorCarte>().title == nom)
            {
				ListSalleBonnes.Remove(ListSalleBonnes[position]);
				suppimer = true;

            }
			position++;
        }
    }

	public void destructionSalleTest()
    {
		
		while (listsalleTest.Count != 0)
		{
			
			Destroy(listsalleTest[0]);
			listsalleTest.RemoveAt(0);
		}

	}

	public void activerBocCollider()
    {
		
			GameObject[] portes = salleActuel.GetComponent<GeneratorCarte>().portes;

			foreach(GameObject porte in portes)
            {
				porte.GetComponent<BoxCollider2D>().enabled = true;
			Debug.Log(" boxColllider actif" );
			}
        
    }

}
