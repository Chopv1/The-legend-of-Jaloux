using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateurMapSalle : MonoBehaviour {

	public int ouverture;
	// 1 pour une porte en haut 
	// 2 pour une porte à droite
	// 3 pour une porte à bas
	// 4 pour une porte à gauche


	private SalleTemplate templates;
	private int rand; // pour aléatoire
	public bool construction = false;

	public float waitTime = 4f;

	void Start(){
		Destroy(gameObject, waitTime);
		//templates = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();
		Invoke("Spawn", 0.99f);
	}


	void Spawn(){
		if(construction == false){
			if(ouverture == 1){
				// une porte en haut 
				rand = Random.Range(0, templates.salleHaut.Length);
				Instantiate(templates.salleHaut[rand], transform.position, templates.salleHaut[rand].transform.rotation);

			} else if(ouverture == 2){
				// pour une porte à droite
				rand = Random.Range(0, templates.salleDroite.Length);
				Instantiate(templates.salleDroite[rand], transform.position, templates.salleDroite[rand].transform.rotation);
				
			} else if(ouverture == 3){
				//  3 pour une porte à bas
				rand = Random.Range(0, templates.salleBas.Length);
				Instantiate(templates.salleBas[rand], transform.position, templates.salleBas[rand].transform.rotation);
				
			} else if(ouverture == 4){
				//  4 pour une porte à gauche
				rand = Random.Range(0, templates.salleGauche.Length);
				Instantiate(templates.salleGauche[rand], transform.position, templates.salleGauche[rand].transform.rotation);
				
			}
			construction = true;
		}
	}
	/*
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("SpawnPoint")){ // déclanchement au contacte d'un autre centre de salle 
			if(other.GetComponent<GenerateurMapSalle>().construction == false && construction == false){
				//Instantiate(templates.sallefermer, transform.position, Quaternion.identity);
				Destroy(gameObject);
			} 
			construction = true;
	
		}

	}
*/
}
