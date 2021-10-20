using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjouterSalle : MonoBehaviour {

	private SalleTemplate templates;

	void Start(){

		templates = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();
		templates.salles.Add(this.gameObject);
	}
}
