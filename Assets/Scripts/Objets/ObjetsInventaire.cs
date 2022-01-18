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
    public GameObject ObjetVis�;
    public int IndiceObjetVis�;
    public Items[] listItemsEquip�s;
    public Items ItemsModifi�;

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
        listItemsEquip�s = new Items[] { new Items(null, null, 0), new Items(null, null, 0), new Items(null, null, 0), new Items(null, null, 0), new Items(null, null, 0) };
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
        ObjetVis� = i;
        if (ObjetVis�.name == "Arme" || ObjetVis�.name == "Plastron" || ObjetVis�.name == "Casque" || ObjetVis�.name == "Jambi�res" || ObjetVis�.name == "Bottes")
        {
            Utiliser.SetActive(false);
            Jeter.SetActive(false);
            Desequiper.SetActive(true);
            Equiper.SetActive(false);


        }
        else
        {
            if (ObjetVis�.name == "ObjetInventaire")
            {
                IndiceObjetVis� = 0;
            }
            else
            {
                IndiceObjetVis� = 1;
                NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";

                while (ObjetVis�.name != NomObjet)
                {
                    IndiceObjetVis�++;
                    NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";
                }
            }

            string typeItem = HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem();
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

        if (ObjetVis�.name == "ObjetInventaire")
        {
            IndiceObjetVis� = 0;
        }
        else
        {
            IndiceObjetVis� = 1;
            NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";

            while (ObjetVis�.name != NomObjet)
            {
                IndiceObjetVis�++;
                NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";
            }
        }

        if(HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem() == "Casque")
        {
            if (!(listItemsEquip�s[0].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquip�s[0]);
            }
            listItemsEquip�s[0] = HeroGuerrier.listItems[IndiceObjetVis�];
            Button Casque = GameObject.Find("Casque").GetComponent<Button>();
            Casque.interactable = true;
            GameObject CasqueTexte = GameObject.Find("Casque").transform.GetChild(0).gameObject;
            CasqueTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquip�s[0].getNomItem();
        }

        else if(HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem() == "Armure")
        {
            if (!(listItemsEquip�s[1].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquip�s[1]);
            }
            listItemsEquip�s[1] = HeroGuerrier.listItems[IndiceObjetVis�];
            Button Plastron = GameObject.Find("Plastron").GetComponent<Button>();
            Plastron.interactable = true;
            GameObject PlastronTexte = GameObject.Find("Plastron").transform.GetChild(0).gameObject;
            PlastronTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquip�s[1].getNomItem();
            HeroGuerrier.herosAnimator.SetBool("isMailled", true);
        }
        else if (HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem() == "Jambi�res")
        {
            if (!(listItemsEquip�s[2].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquip�s[2]);
            }
            listItemsEquip�s[2] = HeroGuerrier.listItems[IndiceObjetVis�];
            Button Jambieres = GameObject.Find("Jambi�res").GetComponent<Button>();
            Jambieres.interactable = true;
            GameObject JambieresTexte = GameObject.Find("Jambi�res").transform.GetChild(0).gameObject;
            JambieresTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquip�s[2].getNomItem();
        }
        else if (HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem() == "Bottes")
        {
            if (!(listItemsEquip�s[3].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquip�s[3]);
            }
            listItemsEquip�s[3] = HeroGuerrier.listItems[IndiceObjetVis�];
            Button Bottes = GameObject.Find("Bottes").GetComponent<Button>();
            Bottes.interactable = true;
            GameObject BottesTexte = GameObject.Find("Bottes").transform.GetChild(0).gameObject;
            BottesTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquip�s[3].getNomItem();
        }
        else if (HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem() == "Arme")
        {
            if (!(listItemsEquip�s[4].getNomItem() == null))
            {
                HeroGuerrier.listItems.Add(listItemsEquip�s[4]);
            }
            listItemsEquip�s[4] = HeroGuerrier.listItems[IndiceObjetVis�];
            Button Arme = GameObject.Find("Arme").GetComponent<Button>();
            Arme.interactable = true;
            GameObject ArmeTexte = GameObject.Find("Arme").transform.GetChild(0).gameObject;
            ArmeTexte.GetComponent<UnityEngine.UI.Text>().text = listItemsEquip�s[4].getNomItem();
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
        string typeItem = HeroGuerrier.listItems[IndiceObjetVis�].getTypeItem();

        if(typeItem == "Soin")
        {
            HeroGuerrier.currentPv += HeroGuerrier.listItems[IndiceObjetVis�].getValeurAttributs();
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
        if (ObjetVis�.name == "Casque")
        {
            HeroGuerrier.listItems.Add(listItemsEquip�s[0]);
            listItemsEquip�s[0] = new Items(null, null, 0);
        }
        else if (ObjetVis�.name == "Plastron")
        {
            HeroGuerrier.listItems.Add(listItemsEquip�s[1]);
            listItemsEquip�s[1] = new Items(null, null, 0);
            HeroGuerrier.herosAnimator.SetBool("isMailled", false);
        }
        else if (ObjetVis�.name == "Jambi�res")
        {
            HeroGuerrier.listItems.Add(listItemsEquip�s[2]);
            listItemsEquip�s[2] = new Items(null, null, 0);
        }
        else if (ObjetVis�.name == "Bottes")
        {
            HeroGuerrier.listItems.Add(listItemsEquip�s[3]);
            listItemsEquip�s[3] = new Items(null, null, 0);
        }
        else if (ObjetVis�.name == "Arme")
        {
            HeroGuerrier.listItems.Add(listItemsEquip�s[4]);
            listItemsEquip�s[4] = new Items(null, null, 0);
        }

        

        Button Casque = GameObject.Find(ObjetVis�.name).GetComponent<Button>();
        Casque.interactable = false;
        GameObject CasqueTexte = GameObject.Find(ObjetVis�.name).transform.GetChild(0).gameObject;
        CasqueTexte.GetComponent<UnityEngine.UI.Text>().text = ObjetVis�.name;


        Utiliser.SetActive(false);
        Jeter.SetActive(false);
        Desequiper.SetActive(false);
        Equiper.SetActive(false);
    }

    public void JeterObjet()
    {
        if (ObjetVis�.name == "ObjetInventaire")
        {
            IndiceObjetVis� = 0;
        }
        else
        {
            IndiceObjetVis� = 1;
            NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";

            while (ObjetVis�.name != NomObjet)
            {
                IndiceObjetVis�++;
                NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";
            }
        }

        HeroGuerrier.listItems.RemoveAt(IndiceObjetVis�);

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
        if (ObjetVis�.name == "Casque")
        {
            ObjectAAfficher = listItemsEquip�s[0];
        }
        else if (ObjetVis�.name == "Plastron")
        {
            ObjectAAfficher = listItemsEquip�s[1];
        }
        else if (ObjetVis�.name == "Jambi�res")
        {
            ObjectAAfficher = listItemsEquip�s[2];
        }
        else if (ObjetVis�.name == "Bottes")
        {
            ObjectAAfficher = listItemsEquip�s[3];
        }
        else if (ObjetVis�.name == "Arme")
        {
            ObjectAAfficher = listItemsEquip�s[4];
        }
        else if (ObjetVis�.name == "ObjetInventaire")
        {
            ObjectAAfficher = HeroGuerrier.listItems[0];
        }
        else
        {
            IndiceObjetVis� = 1;
            NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";

            while (ObjetVis�.name != NomObjet)
            {
                IndiceObjetVis�++;
                NomObjet = "ObjetInventaire (" + IndiceObjetVis� + ")";
            }
            ObjectAAfficher = HeroGuerrier.listItems[IndiceObjetVis�];
        }

        GameObject CanvaTitreStatsObjet = GameObject.Find("TitreStatsObjet");
        CanvaTitreStatsObjet.GetComponent<Text>().text = "Stats " + ObjectAAfficher.getNomItem() + " :";

        string typeItem = ObjectAAfficher.getTypeItem();
        GameObject CanvaDetailseStatsObjet = GameObject.Find("DetailsStatsObjet");
        if (typeItem == "Armure" || typeItem == "Casque" || typeItem == "Jambi�res" || typeItem == "Bottes")
        { CanvaDetailseStatsObjet.GetComponent<Text>().text = "Bonus : \n Cet objet fournit : " + ObjectAAfficher.getValeurAttributs() + " points d'armure."; }
        else if(typeItem == "Arme")
        { CanvaDetailseStatsObjet.GetComponent<Text>().text = "Bonus : \n Cet objet fournit : " + ObjectAAfficher.getValeurAttributs() + " points d'attaque."; }
        else if(typeItem == "Soin")
        { CanvaDetailseStatsObjet.GetComponent<Text>().text = "Bonus : \n L'utilisation de cet objet rend : " + ObjectAAfficher.getValeurAttributs() + " points de vie � votre H�ro."; }

        
    }

    public void AfficherStatsHero()
    {
        GameObject CanvaStatsHero = GameObject.Find("DetailsStatsH�ro");
        int AttaqueGuerrier = HeroGuerrier.attack + listItemsEquip�s[4].getValeurAttributs();
        int DefenseGuerrier = HeroGuerrier.defense + listItemsEquip�s[1].getValeurAttributs() + listItemsEquip�s[2].getValeurAttributs() + listItemsEquip�s[3].getValeurAttributs() + listItemsEquip�s[0].getValeurAttributs();
        CanvaStatsHero.GetComponent<Text>().text = "Stats\n----------------\nPV : " + HeroGuerrier.currentPv + "/" + HeroGuerrier.MaxPv + "\nAttaque : " + AttaqueGuerrier + "\nDefense : " + DefenseGuerrier + "\nPA : " + HeroGuerrier.pa;
    }
    public int GetAttaqueHero()
    {
        try
        {
            print(20 + listItemsEquip�s[4].getValeurAttributs());
            return (20 + listItemsEquip�s[4].getValeurAttributs());
        }
        catch
        {
            print(20);
            return (20);
        }
    }
}
