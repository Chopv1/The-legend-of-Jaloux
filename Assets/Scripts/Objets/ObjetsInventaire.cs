using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObjetsInventaire : MonoBehaviour
{
    public Unit HeroGuerrier;
    private int indice = 0;
    private string NomObjet;
    private List<int> listObjets;
    public GameObject ObjetVisé;
    public int IndiceObjetVisé;
    public Items[] listItemsEquipés;
    public Items ItemsModifié;

    public GameObject Equiper;
    public GameObject Desequiper;
    public GameObject Jeter;
    public GameObject Utiliser;
    public Animator herosAnimator;
    // Update is called once per frame

    public AudioSource Songequiper;


    private Button CaseInventaire;
    void Start()
    {
        listItemsEquipés = new Items[] { new Items(null, null, 0), new Items(null, null, 0), new Items(null, null, 0), new Items(null, null, 0), new Items(null, null, 0) };
        Equiper.SetActive(false);
        Desequiper.SetActive(false);
        Jeter.SetActive(false);
        Utiliser.SetActive(false);
        GameObject CanvaTitreStatsObjet = GameObject.Find("TitreStatsObjet");
        CanvaTitreStatsObjet.GetComponent<Text>().text = "";

        GameObject CanvaDetailseStatsObjet = GameObject.Find("DetailsStatsObjet");
        CanvaDetailseStatsObjet.GetComponent<Text>().text = "";
    }

    void Update()
    {
        AfficherStatsHero();
        placageItems();
    }

    public void placageItems()
    {
        if(HeroGuerrier.listItems.Count != 0)
        {
            indice = 0;

            foreach (Items o in HeroGuerrier.listItems)
            {
                if(indice == 0)
                { 
                    NomObjet = "ObjetInventaire"; 
                }
                else 
                { 
                    NomObjet = "ObjetInventaire (" + indice + ")"; 
                }

                CaseInventaire = GameObject.Find(NomObjet).GetComponent<Button>();
                CaseInventaire.interactable = true;
                GameObject TexteObjet = CaseInventaire.transform.GetChild(0).gameObject;
                TexteObjet.GetComponent<UnityEngine.UI.Text>().text = o.getNomItem();

                indice++;
            }
        }

    }

    public void SetSelectionedItem(GameObject i)
    {
        ObjetVisé = i;
        if (ObjetVisé.name == "Arme" || ObjetVisé.name == "Plastron" || ObjetVisé.name == "Casque" || ObjetVisé.name == "Jambières" || ObjetVisé.name == "Bottes")
        {
            Utiliser.SetActive(false);
            Jeter.SetActive(false);
            Desequiper.SetActive(true);
            Equiper.SetActive(false);


        }
        else
        {
            if (ObjetVisé.name == "ObjetInventaire")
            {
                IndiceObjetVisé = 0;
            }
            else
            {
                IndiceObjetVisé = 1;
                NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";

                while (ObjetVisé.name != NomObjet)
                {
                    IndiceObjetVisé++;
                    NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";
                }
            }

            string typeItem = HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem();
            if (typeItem == "Soin")
            {
                Utiliser.SetActive(true);
                Jeter.SetActive(true);
                Desequiper.SetActive(false);
                Equiper.SetActive(false);

            }
            else
            {
                Utiliser.SetActive(false);
                Jeter.SetActive(true);
                Desequiper.SetActive(false);
                Equiper.SetActive(true);

            }
        }
        AfficherStats();
    }

    public void EquiperObjet()
    {

        if (ObjetVisé.name == "ObjetInventaire")
        {
            IndiceObjetVisé = 0;
        }
        else
        {
            IndiceObjetVisé = 1;
            NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";

            while (ObjetVisé.name != NomObjet)
            {
                IndiceObjetVisé++;
                NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";
            }
        }

        if(HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem() == "Casque")
        {
            if (!(listItemsEquipés[0].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquipés[0]);
            }
            listItemsEquipés[0] = HeroGuerrier.listItems[IndiceObjetVisé];
            Button Casque = GameObject.Find("Casque").GetComponent<Button>();
            Casque.interactable = true;
            GameObject CasqueTexte = GameObject.Find("Casque").transform.GetChild(0).gameObject;
            CasqueTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquipés[0].getNomItem();
        }

        else if(HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem() == "Armure")
        {
            if (!(listItemsEquipés[1].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquipés[1]);
            }
            listItemsEquipés[1] = HeroGuerrier.listItems[IndiceObjetVisé];
            Button Plastron = GameObject.Find("Plastron").GetComponent<Button>();
            Plastron.interactable = true;
            GameObject PlastronTexte = GameObject.Find("Plastron").transform.GetChild(0).gameObject;
            PlastronTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquipés[1].getNomItem();
            HeroGuerrier.herosAnimator.SetBool("isMailled", true);
        }
        else if (HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem() == "Jambières")
        {
            if (!(listItemsEquipés[2].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquipés[2]);
            }
            listItemsEquipés[2] = HeroGuerrier.listItems[IndiceObjetVisé];
            Button Jambieres = GameObject.Find("Jambières").GetComponent<Button>();
            Jambieres.interactable = true;
            GameObject JambieresTexte = GameObject.Find("Jambières").transform.GetChild(0).gameObject;
            JambieresTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquipés[2].getNomItem();
        }
        else if (HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem() == "Bottes")
        {
            if (!(listItemsEquipés[3].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquipés[3]);
            }
            listItemsEquipés[3] = HeroGuerrier.listItems[IndiceObjetVisé];
            Button Bottes = GameObject.Find("Bottes").GetComponent<Button>();
            Bottes.interactable = true;
            GameObject BottesTexte = GameObject.Find("Bottes").transform.GetChild(0).gameObject;
            BottesTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquipés[3].getNomItem();
        }
        else if (HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem() == "Arme")
        {
            if (!(listItemsEquipés[4].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquipés[4]);
            }
            listItemsEquipés[4] = HeroGuerrier.listItems[IndiceObjetVisé];
            Button Arme = GameObject.Find("Arme").GetComponent<Button>();
            Arme.interactable = true;
            GameObject ArmeTexte = GameObject.Find("Arme").transform.GetChild(0).gameObject;
            ArmeTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquipés[4].getNomItem();
        }
        Songequiper.Play();
        JeterObjet();
        Utiliser.SetActive(false);
        Jeter.SetActive(false);
        Desequiper.SetActive(false);
        Equiper.SetActive(false);
    }

    public void UtiliserObjet()
    {
        string typeItem = HeroGuerrier.listItems[IndiceObjetVisé].getTypeItem();

        if(typeItem == "Soin")
        {
            HeroGuerrier.currentPv += HeroGuerrier.listItems[IndiceObjetVisé].getValeurAttributs();
            if(HeroGuerrier.currentPv > HeroGuerrier.MaxPv)
            {
                HeroGuerrier.currentPv = HeroGuerrier.MaxPv;
            }
        }
        JeterObjet();
        Utiliser.SetActive(false);
        Jeter.SetActive(false);
        Desequiper.SetActive(false);
        Equiper.SetActive(false);

    }

    public void DesequiperObjet()
    {
        if (ObjetVisé.name == "Casque")
        {
            HeroGuerrier.listItems.Add(listItemsEquipés[0]);
            listItemsEquipés[0] = new Items(null, null, 0);
        }
        else if (ObjetVisé.name == "Plastron")
        {
            HeroGuerrier.listItems.Add(listItemsEquipés[1]);
            listItemsEquipés[1] = new Items(null, null, 0);
            HeroGuerrier.herosAnimator.SetBool("isMailled", false);
        }
        else if (ObjetVisé.name == "Jambières")
        {
            HeroGuerrier.listItems.Add(listItemsEquipés[2]);
            listItemsEquipés[2] = new Items(null, null, 0);
        }
        else if (ObjetVisé.name == "Bottes")
        {
            HeroGuerrier.listItems.Add(listItemsEquipés[3]);
            listItemsEquipés[3] = new Items(null, null, 0);
        }
        else if (ObjetVisé.name == "Arme")
        {
            HeroGuerrier.listItems.Add(listItemsEquipés[4]);
            listItemsEquipés[4] = new Items(null, null, 0);
        }

        

        Button Casque = GameObject.Find(ObjetVisé.name).GetComponent<Button>();
        Casque.interactable = false;
        GameObject CasqueTexte = GameObject.Find(ObjetVisé.name).transform.GetChild(0).gameObject;
        CasqueTexte.GetComponent<UnityEngine.UI.Text>().text = ObjetVisé.name;


        Utiliser.SetActive(false);
        Jeter.SetActive(false);
        Desequiper.SetActive(false);
        Equiper.SetActive(false);
    }

    public void JeterObjet()
    {
        if (ObjetVisé.name == "ObjetInventaire")
        {
            IndiceObjetVisé = 0;
        }
        else
        {
            IndiceObjetVisé = 1;
            NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";

            while (ObjetVisé.name != NomObjet)
            {
                IndiceObjetVisé++;
                NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";
            }
        }

        HeroGuerrier.listItems.RemoveAt(IndiceObjetVisé);

        if (HeroGuerrier.listItems.Count == 0)
        {
            NomObjet = "ObjetInventaire";
        }
        else
        {
            NomObjet = "ObjetInventaire (" + HeroGuerrier.listItems.Count + ")";
        }

        CaseInventaire = GameObject.Find(NomObjet).GetComponent<Button>();
        CaseInventaire.interactable = false;
        GameObject TexteObjet = CaseInventaire.transform.GetChild(0).gameObject;
        TexteObjet.GetComponent<UnityEngine.UI.Text>().text = "";

        Utiliser.SetActive(false);
        Jeter.SetActive(false);
        Desequiper.SetActive(false);
        Equiper.SetActive(false);

        GameObject CanvaTitreStatsObjet = GameObject.Find("TitreStatsObjet");
        CanvaTitreStatsObjet.GetComponent<Text>().text = "";

        GameObject CanvaDetailseStatsObjet = GameObject.Find("DetailsStatsObjet");
        CanvaDetailseStatsObjet.GetComponent<Text>().text = "";
    }

    public void AfficherStats()
    {
        Items ObjectAAfficher;
        if (ObjetVisé.name == "Casque")
        {
            ObjectAAfficher = listItemsEquipés[0];
        }
        else if (ObjetVisé.name == "Plastron")
        {
            ObjectAAfficher = listItemsEquipés[1];
        }
        else if (ObjetVisé.name == "Jambières")
        {
            ObjectAAfficher = listItemsEquipés[2];
        }
        else if (ObjetVisé.name == "Bottes")
        {
            ObjectAAfficher = listItemsEquipés[3];
        }
        else if (ObjetVisé.name == "Arme")
        {
            ObjectAAfficher = listItemsEquipés[4];
        }
        else if (ObjetVisé.name == "ObjetInventaire")
        {
            ObjectAAfficher = HeroGuerrier.listItems[0];
        }
        else
        {
            IndiceObjetVisé = 1;
            NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";

            while (ObjetVisé.name != NomObjet)
            {
                IndiceObjetVisé++;
                NomObjet = "ObjetInventaire (" + IndiceObjetVisé + ")";
            }
            ObjectAAfficher = HeroGuerrier.listItems[IndiceObjetVisé];
        }

        GameObject CanvaTitreStatsObjet = GameObject.Find("TitreStatsObjet");
        CanvaTitreStatsObjet.GetComponent<Text>().text = "Stats " + ObjectAAfficher.getNomItem() + " :";

        string typeItem = ObjectAAfficher.getTypeItem();
        GameObject CanvaDetailseStatsObjet = GameObject.Find("DetailsStatsObjet");
        if (typeItem == "Armure" || typeItem == "Casque" || typeItem == "Jambières" || typeItem == "Bottes")
        { CanvaDetailseStatsObjet.GetComponent<Text>().text = "Bonus : \n Cet objet fournit : " + ObjectAAfficher.getValeurAttributs() + " points d'armure."; }
        else if(typeItem == "Arme")
        { CanvaDetailseStatsObjet.GetComponent<Text>().text = "Bonus : \n Cet objet fournit : " + ObjectAAfficher.getValeurAttributs() + " points d'attaque."; }
        else if(typeItem == "Soin")
        { CanvaDetailseStatsObjet.GetComponent<Text>().text = "Bonus : \n L'utilisation de cet objet rend : " + ObjectAAfficher.getValeurAttributs() + " points de vie à votre Héro."; }

        
    }

    public void AfficherStatsHero()
    {
        GameObject CanvaStatsHero = GameObject.Find("DetailsStatsHéro");
        int AttaqueGuerrier = HeroGuerrier.attack + listItemsEquipés[4].getValeurAttributs();
        int DefenseGuerrier = HeroGuerrier.defense + listItemsEquipés[1].getValeurAttributs() + listItemsEquipés[2].getValeurAttributs() + listItemsEquipés[3].getValeurAttributs() + listItemsEquipés[0].getValeurAttributs();
        CanvaStatsHero.GetComponent<Text>().text = "Stats\n----------------\nPV : " + HeroGuerrier.currentPv + "/" + HeroGuerrier.MaxPv + "\nAttaque : " + AttaqueGuerrier + "\nDefense : " + DefenseGuerrier + "\nPA : " + HeroGuerrier.pa;
    }
    public int GetAttaqueHero()
    {
        try
        {
            print(20 + listItemsEquipés[4].getValeurAttributs());
            return (20 + listItemsEquipés[4].getValeurAttributs());
        }
        catch
        {
            print(20);
            return (20);
        }
    }
}
