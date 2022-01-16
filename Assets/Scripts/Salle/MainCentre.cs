using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCentre : MonoBehaviour
{
    public bool mainPosable;
    public List<GameObject> centres;

    // Start is called before the first frame update
    void Start()
    {
        mainPosable = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void miseAjourPosable()
    {
        int nombreOUverture = centres.Count;
        int ouverture= 0;
        while( mainPosable && ouverture < nombreOUverture)
        {
            if (!centres[ouverture].GetComponent<InfoCentreSalle>().getPosable())
            {
                mainPosable = false;
                Debug.Log(" ouverture " + ouverture + " est " + centres[ouverture].GetComponent<InfoCentreSalle>().getPosable()) ;
            }
            ouverture++;
        }

    }

    public void setMainPosable(bool posable)
    {
        mainPosable = posable;
    }
    public bool getMainPosable()
    {
        miseAjourPosable();
        return mainPosable;
    }

    public void MiseAjourCentreSignature()
    {
        Debug.Log("ds main Cnetr");
        foreach(GameObject centre in centres)
        {
            centre.GetComponent<InfoCentreSalle>().MiseAjourCentre();
            
        }
    }
    public void changerTagTest()
    {
        foreach (GameObject centre in centres)
        {
            centre.tag="Test";

        }
    }
    public void changerTagSpwan()
    {
        foreach (GameObject centre in centres)
        {
            centre.tag = "SpawnPoint";

        }
    }
}
