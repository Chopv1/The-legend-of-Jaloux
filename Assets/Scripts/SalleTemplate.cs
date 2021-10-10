using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalleTemplate : MonoBehaviour
{
	public GameObject[] salleBas;
	public GameObject[] salleHaut;
	public GameObject[] salleGauche;
	public GameObject[] salleDroite;

	public GameObject sallefermer;

	public List<GameObject> salles; // liste de toutes les salles

	public float tempsAttente;
	private bool spawnedBoss;
	public GameObject boss;

	void Update()
	{

		if (tempsAttente <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < 4 /*salles.Count*/; i++)
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
	}
}
