using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prehistory : MonoBehaviour {

    ScreenEffects screenEffects;
    PrintText printText;
    public Animator prehistoryController;

    // Use this for initialization
    /*void Start ()
    {
        screenEffects = GetComponent<ScreenEffects>();
        printText = GetComponent<PrintText>();
        screenEffects.alphaValue = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (screenEffects.alphaValue <= 0 & screenEffects.blackScreen.activeInHierarchy == true)
        {
            screenEffects.BlackScreenDeactivate();
            printText.StartPrint();
        }
        if (printText.charNumber == printText.duplicateText.Length)
            Invoke("Blackout", printText.delay);
        else
            screenEffects.LightingScreen();
        LoadingLogoScreen();
        if (printText.charNumber == 207)
            prehistoryController.SetBool("ActiveEyes", true);
    }

    // Loading new scene "LogoScrean"
    void LoadingLogoScreen()
    {
        if (screenEffects.alphaValue >= 1)
            SceneManager.LoadScene(3);
    }

    // Activates the component "Black Screen" in the editor
    void Blackout()
    {
        screenEffects.BlackScreenActive();
        screenEffects.BlackoutScreen();
    }*/
}
