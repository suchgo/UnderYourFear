using UnityEngine;
using UnityEngine.UI;

public class PaperForText : MonoBehaviour {

    public GameObject PaperGameObject;
    public Image closePaper;
    public Text paperText;
    bool closeButtonPressed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OpenPaperList();
        ClosePaperList();
    }

    void OpenPaperList()
    {
        if (PaperGameObject.activeInHierarchy && !closeButtonPressed && PaperGameObject.GetComponent<Image>().color.a <= 1f)
        {
            PaperGameObject.GetComponent<Image>().color = new Color(PaperGameObject.GetComponent<Image>().color.r, PaperGameObject.GetComponent<Image>().color.g, PaperGameObject.GetComponent<Image>().color.b, PaperGameObject.GetComponent<Image>().color.a + 0.05f);
            closePaper.color = new Color(closePaper.color.r, closePaper.color.g, closePaper.color.b, closePaper.color.a + 0.05f);
            paperText.color = new Color(paperText.color.r, paperText.color.g, paperText.color.b, paperText.color.a + 0.05f);
        }
    }

    void ClosePaperList()
    {
        if (closeButtonPressed)
        {
            PaperGameObject.GetComponent<Image>().color = new Color(PaperGameObject.GetComponent<Image>().color.r, PaperGameObject.GetComponent<Image>().color.g, PaperGameObject.GetComponent<Image>().color.b, PaperGameObject.GetComponent<Image>().color.a - 0.05f);
            closePaper.color = new Color(closePaper.color.r, closePaper.color.g, closePaper.color.b, closePaper.color.a - 0.05f);
            paperText.color = new Color(paperText.color.r, paperText.color.g, paperText.color.b, paperText.color.a - 0.05f);
            if (PaperGameObject.GetComponent<Image>().color.a <= 0)
            {
                PaperGameObject.SetActive(false);
                closeButtonPressed = false;
            } 
        }
    }

    public void CloseButtonPresed()
    {
        closeButtonPressed = true;
    }

    public void OpenPaperListCall()
    {
        PaperGameObject.SetActive(true);
    }
}
