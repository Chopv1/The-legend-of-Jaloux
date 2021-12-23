using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    private int HeroLevel;
    private int ExperienceHero;
    public int PointCompetence;
    public int NiveauSort1 = 1;
    public int NiveauSort2 = 1;
    public int NiveauSort3 = 1;
    public GameObject LevelUpObject;
    public GameObject LevelObject;
    public GameObject pointsCompetence;
    public GameObject Inventory;
    public GameObject ArbreCompetence;

    public Button Sort1Amelioration1;
    public Button Sort1Amelioration2;
    public Button Sort1Amelioration3;

    public Button Sort2Amelioration1;
    public Button Sort2Amelioration2;
    public Button Sort2Amelioration3;

    public Button Sort3Amelioration1;
    public Button Sort3Amelioration2;
    public Button Sort3Amelioration3;

    private bool isCoroutineExecuting = false;
    private bool isOpenComp = false;
    private bool isOpenInventory = false;

    void Start()
    {
        HeroLevel = 1;
        ExperienceHero = 0;
        PointCompetence = 0;
    }
    void Update() 
    {
        pointsCompetence.GetComponent<UnityEngine.UI.Text>().text = PointCompetence.ToString();
        coloriser();
    }

    public void MonstreTue(int levelMonstre) 
    {
        ExperienceHero += levelMonstre;
        if(ExperienceHero >= 100) 
        {
            HeroLevel = HeroLevel + 1;
            PointCompetence += 2;
            ExperienceHero = ExperienceHero - 100;
            LevelObject.GetComponent<UnityEngine.UI.Text>().text = HeroLevel.ToString();
            LevelUpObject.SetActive(true);
            StartCoroutine(ExecuteAfterTime(2)); ;
            
        }   
    
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        LevelUpObject.SetActive(false);
    }

    public void OpenCloseInventory()
    {
        if(isOpenInventory)
        {
            Inventory.SetActive(false);
            isOpenInventory = false;
        }
        else
        {
            Inventory.SetActive(true);
            isOpenInventory = true;
        }
    }
    public void OpenCloseArbre()
    {
        if (isOpenComp)
        {
            ArbreCompetence.SetActive(false);
            isOpenComp = false;
        }
        else
        {
            ArbreCompetence.SetActive(true);
            isOpenComp = true;
        }
    }
    public void Amelioration1Sort1()
    {
        if(PointCompetence >= 3 && NiveauSort1 == 1)
        {
            PointCompetence -= 3;
            NiveauSort1 = 2;
        }

    }
    public void Amelioration2Sort1()
    {
        if (PointCompetence >= 3 && NiveauSort1 == 2)
        {
            PointCompetence -= 3;
            NiveauSort1 = 3;
        }
    }
    public void Amelioration3Sort1()
    {
        if (PointCompetence >= 8 && NiveauSort1 == 3)
        {
            PointCompetence -= 8;
            NiveauSort1 = 4;
        }
    }



    public void Amelioration1Sort2()
    {
        if (PointCompetence >= 5 && NiveauSort2 == 1)
        {
            PointCompetence -= 5;
            NiveauSort2 = 2;
        }

    }
    public void Amelioration2Sort2()
    {
        if (PointCompetence >= 6 && NiveauSort2 == 2)
        {
            PointCompetence -= 6;
            NiveauSort2 = 3;
        }
    }
    public void Amelioration3Sort2()
    {
        if (PointCompetence >= 10 && NiveauSort2 == 3)
        {
            PointCompetence -= 10;
            NiveauSort2 = 4;
        }
    }



    public void Amelioration1Sort3()
    {
        if (PointCompetence >= 6 && NiveauSort3 == 1)
        {
            PointCompetence -= 6;
            NiveauSort3 = 2;
        }

    }
    public void Amelioration2Sort3()
    {
        if (PointCompetence >= 7 && NiveauSort3 == 2)
        {
            PointCompetence -= 7;
            NiveauSort3 = 3;
        }
    }
    public void Amelioration3Sort3()
    {
        if (PointCompetence >= 8 && NiveauSort3 == 3)
        {
            PointCompetence -= 8;
            NiveauSort3 = 4;
        }
    }





    public void coloriser()
    {
        var colors = Sort1Amelioration2.colors;
        if (NiveauSort1 == 1)
        {
            colors = Sort1Amelioration2.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort1Amelioration2.colors = colors;

            colors = Sort1Amelioration3.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort1Amelioration3.colors = colors;


            if (PointCompetence < 3)
            {
                colors = Sort1Amelioration1.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort1Amelioration1.colors = colors;
            }
            else
            {
                colors = Sort1Amelioration1.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort1Amelioration1.colors = colors;

            }
        }
        if (NiveauSort1 == 2)
        {
            colors = Sort1Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort1Amelioration1.colors = colors;

            colors = Sort1Amelioration3.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort1Amelioration3.colors = colors;


            if (PointCompetence < 3)
            {
                colors = Sort1Amelioration2.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort1Amelioration2.colors = colors;
            }
            else
            {
                colors = Sort1Amelioration2.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort1Amelioration2.colors = colors;

            }
        }
        if (NiveauSort1 == 3)
        {
            colors = Sort1Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort1Amelioration1.colors = colors;

            colors = Sort1Amelioration2.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort1Amelioration2.colors = colors;


            if (PointCompetence < 8)
            {
                colors = Sort1Amelioration3.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort1Amelioration3.colors = colors;
            }
            else
            {
                colors = Sort1Amelioration3.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort1Amelioration3.colors = colors;

            }
        }
        if (NiveauSort1 == 4)
        {
            colors = Sort1Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort1Amelioration1.colors = colors;

            colors = Sort1Amelioration2.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort1Amelioration2.colors = colors;

            colors = Sort1Amelioration3.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort1Amelioration3.colors = colors;
        }
        if (NiveauSort2 == 1)
        {
            colors = Sort2Amelioration2.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort2Amelioration2.colors = colors;

            colors = Sort2Amelioration3.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort2Amelioration3.colors = colors;


            if (PointCompetence < 5)
            {
                colors = Sort2Amelioration1.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort2Amelioration1.colors = colors;
            }
            else
            {
                colors = Sort2Amelioration1.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort2Amelioration1.colors = colors;

            }
        }
        if (NiveauSort2 == 2)
        {
            colors = Sort2Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort2Amelioration1.colors = colors;

            colors = Sort2Amelioration3.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort2Amelioration3.colors = colors;


            if (PointCompetence < 6)
            {
                colors = Sort2Amelioration2.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort2Amelioration2.colors = colors;
            }
            else
            {
                colors = Sort2Amelioration2.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort2Amelioration2.colors = colors;

            }
        }
        if (NiveauSort2 == 3)
        {
            colors = Sort2Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort2Amelioration1.colors = colors;

            colors = Sort2Amelioration2.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort2Amelioration2.colors = colors;


            if (PointCompetence < 10)
            {
                colors = Sort2Amelioration3.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort2Amelioration3.colors = colors;
            }
            else
            {
                colors = Sort2Amelioration3.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort2Amelioration3.colors = colors;

            }
        }
        if (NiveauSort2 == 4)
        {
            colors = Sort2Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort2Amelioration1.colors = colors;

            colors = Sort2Amelioration2.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort2Amelioration2.colors = colors;

            colors = Sort2Amelioration3.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort2Amelioration3.colors = colors;
        }
        if (NiveauSort3 == 1)
        {
            colors = Sort3Amelioration2.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort3Amelioration2.colors = colors;

            colors = Sort3Amelioration3.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort3Amelioration3.colors = colors;


            if (PointCompetence < 6)
            {
                colors = Sort3Amelioration1.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort3Amelioration1.colors = colors;
            }
            else
            {
                colors = Sort3Amelioration1.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort3Amelioration1.colors = colors;

            }
        }
        if (NiveauSort3 == 2)
        {
            colors = Sort3Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort3Amelioration1.colors = colors;

            colors = Sort3Amelioration3.colors;
            colors.highlightedColor = Color.grey;
            colors.normalColor = Color.grey;
            colors.pressedColor = Color.grey;
            colors.selectedColor = Color.grey;
            Sort3Amelioration3.colors = colors;


            if (PointCompetence < 7)
            {
                colors = Sort3Amelioration2.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort3Amelioration2.colors = colors;
            }
            else
            {
                colors = Sort3Amelioration2.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort3Amelioration2.colors = colors;

            }
        }
        if (NiveauSort3 == 3)
        {
            colors = Sort3Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort3Amelioration1.colors = colors;

            colors = Sort3Amelioration2.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort3Amelioration2.colors = colors;


            if (PointCompetence < 8)
            {
                colors = Sort3Amelioration3.colors;
                colors.highlightedColor = Color.red;
                colors.normalColor = Color.red;
                colors.pressedColor = Color.red;
                colors.selectedColor = Color.red;
                Sort3Amelioration3.colors = colors;
            }
            else
            {
                colors = Sort3Amelioration3.colors;
                colors.normalColor = Color.white;
                colors.pressedColor = Color.grey;
                colors.highlightedColor = Color.grey;
                Sort3Amelioration3.colors = colors;

            }
        }
        if (NiveauSort3 == 4)
        {
            colors = Sort3Amelioration1.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort3Amelioration1.colors = colors;

            colors = Sort3Amelioration2.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort3Amelioration2.colors = colors;

            colors = Sort3Amelioration3.colors;
            colors.highlightedColor = Color.black;
            colors.normalColor = Color.black;
            colors.pressedColor = Color.black;
            colors.selectedColor = Color.black;
            Sort3Amelioration3.colors = colors;
        }
    }

}
