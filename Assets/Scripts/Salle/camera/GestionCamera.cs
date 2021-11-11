using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCamera : MonoBehaviour

{
    public GameObject panel;
  
    /*
    
    public GameObject cameraMain;

    public GameObject cameraCartes;
    public AudioListener audio1;
    public AudioListener audio2;
    
    */
    // Start is called before the first frame update
    void Start()
    {
       panel.SetActive(false);
        //GameObject.FindGameObjectWithTag("MapCarte").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Get the screen position :
        Vector3 mouseInScreen = Input.mousePosition;
        // Set the distance from the camera :
        mouseInScreen.z = 10;  // The distance from the camera is 10
                               // Get the world position :
        Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInScreen);

        // Aplly the position to the transform
        transform.position = mouseInWorld;
        Debug.Log(mouseInWorld.x);
        */
    }
    public void changerCarte()
    {
       panel.SetActive(true);
    }
    public void changerMap()
    {
        panel.SetActive(false);
    }


    /*
    public void changerCameraCarte()
    {
        Debug.Log(" camera Carte ");
        cameraMain.SetActive(false);
        audio1.enabled = false;

        cameraCartes.SetActive(true);
        audio2.enabled = true;

    }

    public void changerCameraMain()
    {
        cameraMain.SetActive(true);
        audio1.enabled = true;

        cameraCartes.SetActive(false);
        audio2.enabled = false;


    }
    */
}
