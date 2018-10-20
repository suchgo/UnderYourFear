using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour {

    public GameObject blackScreen;
    public bool isBlackoutScreen;
    public float rateOfChangeValue = 0.01f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    // Changes the transparency of the object "Black Screen" from 1 to 0
    public void BlackoutScreen()
    {
        if (!isBlackoutScreen)
        {
            if (!blackScreen.activeInHierarchy)
                blackScreen.SetActive(true);
            if (blackScreen.GetComponent<Image>().color.a < 1)
            {
                blackScreen.GetComponent<Image>().color = new Color(blackScreen.GetComponent<Image>().color.r, blackScreen.GetComponent<Image>().color.g, blackScreen.GetComponent<Image>().color.b, blackScreen.GetComponent<Image>().color.a + rateOfChangeValue);
            }
            else
                isBlackoutScreen = true; 
        }
    }

    // Changes the transparency of the object "Black Screen" from 0 to 1
    public void LightingScreen()
    {
        if (blackScreen.GetComponent<Image>().color.a > 0)
        {
            blackScreen.GetComponent<Image>().color = new Color(blackScreen.GetComponent<Image>().color.r, blackScreen.GetComponent<Image>().color.g, blackScreen.GetComponent<Image>().color.b, blackScreen.GetComponent<Image>().color.a - rateOfChangeValue);
        }
        else
        {
            if (blackScreen.activeInHierarchy)
                blackScreen.SetActive(false);
            isBlackoutScreen = false;
        }    
    }
}
