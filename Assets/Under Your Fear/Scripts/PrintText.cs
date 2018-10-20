using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintText : MonoBehaviour {

    public int delay, startDelay, charNumber = 0;
    public float speed;
    public Text textObject;
    public string duplicateText;
    bool runDelay = false;
    
    // Use this for initialization
    void Start () {
        duplicateText = textObject.text;
        textObject.text = "";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Character-typing the text in the component "Text"
    public void Typewriter()
    {
        if (charNumber < duplicateText.Length & runDelay == false)
        {
            if (duplicateText[charNumber] == '\n')
            {
                runDelay = true;
                Invoke("ClearTextObject", delay);
            }
            else
            {
                textObject.text += duplicateText[charNumber];
            }
            charNumber++;
        }
    }

    // Cleans component "Text"
    void ClearTextObject()
    {
        textObject.text = "";
        runDelay = false;
    }

    // Start to print text
    public void StartPrint()
    {
        InvokeRepeating("Typewriter", startDelay, speed);
    }
}
