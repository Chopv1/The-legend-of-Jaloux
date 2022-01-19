using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCamera : MonoBehaviour

{
    public GameObject panel;
    public AudioSource SonBouton;

    public GameObject MainCamera;
  
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

    public void PlaySonBouton()
    {
        SonBouton.Play();
    }
    public void changerSalle(GameObject centre, GameObject porte)
    {



        switch (porte.GetComponent<HeroCreationSalle>().ouverture)
        { // [ H,D,B,G]
            case 1:
                // demande ouverture pour haut donc ouverture par le bas  
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, centre.transform.position.y-1, -1);
                break;
            case 2:
                // demande ouverture par la gauche donc ouverture par la droite
                MainCamera.transform.position = new Vector3(centre.transform.position.x-1, MainCamera.transform.position.y, -1);
                break;
            case 3:
                 // demande ouverture pour bas donc ouverture par le haut
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, centre.transform.position.y+1, -1);
                break;
            case 4:
                // demade ouverture par la droite donc ouverture par la gauche
                MainCamera.transform.position = new Vector3(centre.transform.position.x+1, MainCamera.transform.position.y, -1);
                break;
            default:
                break;

        }
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
