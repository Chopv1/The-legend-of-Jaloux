using System.Collections;
using UnityEngine;



    public class Main : MonoBehaviour
    {
        private SalleTemplate templates;

        // Use this for initialization
        void Start()
        {
            templates = GameObject.FindGameObjectWithTag("Salle").GetComponent<SalleTemplate>();
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void enregistrementSalles()
        {
            templates.GetComponent<SalleTemplate>().enregistrementSignature();
        }
    }