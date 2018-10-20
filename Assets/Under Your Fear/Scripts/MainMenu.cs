using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    bool buttonPressed = false;
    ScreenEffects screenEffects;

    // Use this for initialization
    void Start () {
        screenEffects = GetComponent<ScreenEffects>();
    }
	
	// Update is called once per frame
	void Update () {
        LoadNewScene();
    }

    void LoadNewScene()
    {
        if (buttonPressed)
        {
            screenEffects.BlackoutScreen();
            this.GetComponent<AudioSource>().volume -= 0.01f;
            if (screenEffects.isBlackoutScreen)
                SceneManager.LoadScene("Game");
        }
    }

    public void StartButtonFunction(Button _button)
    {
        buttonPressed = true;
        _button.interactable = false;
    }
}
