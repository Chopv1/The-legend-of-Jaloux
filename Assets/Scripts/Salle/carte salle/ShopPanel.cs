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
    public SalleInfo salle;
    public bool changer; 

    private void Start()
    {
        titleTxt.text = salle.title;
        descriptionTxt.text = salle.description;
        pointActionTxt.text = salle.pointAction.ToString();
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

    public void ChangerCarte(GeneratorCarte carte)
    {
        titleTxt.text = carte.title;
        descriptionTxt.text = carte.description;
        pointActionTxt.text = carte.pointAction.ToString();
        changer = true;
        Update();


    }

}
