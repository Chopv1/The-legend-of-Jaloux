using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private int HeroLevel;
    private int ExperienceHero;
    private int PointCompetence;
    public GameObject LevelUpObject;
    public GameObject LevelObject;
    public GameObject Inventory;
    public GameObject ArbreCompetence;
    private bool isCoroutineExecuting = false;
    private bool isOpenComp = false;
    private bool isOpenInventory = false;

    // Start is called before the first frame update
    void Start()
    {
        HeroLevel = 1;
        ExperienceHero = 0;
        PointCompetence = 0;
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

}
