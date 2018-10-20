using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public GameObject textPanel, mainCharacter, clickedObject, progressBar;
    public Text textObject;
    public bool clickOnObject = false, useItemForProgress = false;
    public Image progressBarValue;
    public byte progressStatus = 0;
    public enum progressStatusList { NotReady, Ready, Waiting, Complite }
    float startTime, progressBarSpeed = 1f;
    bool textShows = false, stopBasicFunction = false, usePermitted = false;
    string[] rowNumber, unsuitablePhrases;
    byte booksFunctionStatus = 0, cupboardFunctionStatus = 0, flasketFunctionStatus = 0, bathFunctionStatus = 0, /*AЛО*/ safeFunctionStatus = 0;
    Inventory inventory;
    DistanceToTheObject distanceToTheObject;

    // Use this for initialization
    void Start () {
        inventory = GetComponent<Inventory>();
        distanceToTheObject = GetComponent<DistanceToTheObject>();
    }

    // Update is called once per frame
    void Update () {
        HideText();
        СharacterCameToTheObject();
        ProgressBar();
    }

    void СharacterCameToTheObject()
    {
        if (clickOnObject == true && clickedObject != null)
        {
            if ((mainCharacter.transform.position.x - clickedObject.transform.position.x <= distanceToTheObject.distance && mainCharacter.transform.position.x - clickedObject.transform.position.x >= 0f) || (mainCharacter.transform.position.x - clickedObject.transform.position.x >= -distanceToTheObject.distance && mainCharacter.transform.position.x - clickedObject.transform.position.x <= 0f))
            {
                clickOnObject = false;
                clickedObject.GetComponent<Button>().interactable = false;
                FlasketFunction();
                BathAnimation();
                UseItemOnObject();
                if (stopBasicFunction)
                {
                    stopBasicFunction = false;
                    return;
                }
                BathFunction();
                BooksFunction();
                OilFunction();
                CloseInTheFunction();
                Safe();
                CupboardFunction();
                ShowText();
                CupboardAnimation();
            }
        
        }
    }

    public void SelectObject()
    {
        clickOnObject = true;
        textPanel.SetActive(false);
        progressStatus = (byte)progressStatusList.NotReady;
        if (inventory.inventoryOpen)
            inventory.CallInventory();
        distanceToTheObject.SetDistanceToTheObkect();
    }

    public void ShowText()
    {
        if (progressStatus != (byte)progressStatusList.Ready && progressStatus != (byte)progressStatusList.Waiting)
        {
            textPanel.SetActive(true);
            startTime = Time.time;
            textShows = true;
        }
    }

    void HideText()
    {
        if (textShows == true && (Time.time - startTime) >= 5)
        {
            textShows = false;
            textPanel.SetActive(false);
            if (clickedObject != null && clickedObject.GetComponent<Button>() != null)
                clickedObject.GetComponent<Button>().interactable = true;
        }
    }

    public void SelectData(GameObject currentObject)
    {
        int i = 0, counter = 0;
        foreach (List<KeyValuePair<string, object>> row in DatabaseService.ExecuteCommand("SELECT Text FROM Objects WHERE Name = '" + currentObject.name + "';"))
            rowNumber = new string[++counter];
        foreach (List<KeyValuePair<string, object>> row in DatabaseService.ExecuteCommand("SELECT Text FROM Objects WHERE Name = '" + currentObject.name + "';"))
            foreach (KeyValuePair<string, object> col in row)
                rowNumber[i++] = col.Value.ToString();
        counter = 0;
        i = 0;
        if (clickedObject != null && clickedObject.GetComponent<Button>() != null)
            if (clickedObject.GetComponent<Button>().interactable == false)
                clickedObject.GetComponent<Button>().interactable = true;
        clickedObject = currentObject;
    }

    void CupboardFunction()
    {
        if (clickedObject.name == "Cupboard")
        {
            if (usePermitted == false && cupboardFunctionStatus == 0)
                textObject.text = rowNumber[0];
            else
            if(cupboardFunctionStatus == 0)
                cupboardFunctionStatus++;
            if (cupboardFunctionStatus == 1)
            {
                textObject.text = rowNumber[1];
                cupboardFunctionStatus++;
                inventory.AddItemToInventory("FathersLetter", null, "Письмо облитое кофе.", "Icons/FathersLetter", ref cupboardFunctionStatus, (byte)Inventory.itemType.textItem);
            }
            else
            if (cupboardFunctionStatus == 2)
                textObject.text = rowNumber[new System.Random().Next(2, rowNumber.Length)];
        }

    }

    void CupboardAnimation()
    {
        if (clickedObject.name == "Cupboard")
        {
            if (usePermitted == false)
            {
                clickedObject.GetComponent<Animator>().SetBool("BottomDrawerOpen", true);
            }
            else
            {
                clickedObject.GetComponent<Animator>().SetBool("TopDrawerOpen", true);
                clickedObject.GetComponent<Animator>().SetBool("TopBottomOpen", true);
            }
        }
    }

    public void CandleFunction()
    {
        textObject.text = rowNumber[new System.Random().Next(0, rowNumber.Length)];
    }

    void BooksFunction()
    {
        if (clickedObject.name == "Books")
        {
            if (booksFunctionStatus == 0)
            {
                textObject.text = rowNumber[0];
                booksFunctionStatus++;
            }
            else
            if (booksFunctionStatus == 1)
            {
                if (progressStatus == (byte)progressStatusList.Complite)
                {
                    textObject.text = rowNumber[1];
                    inventory.AddItemToInventory("KeyToTheCupboard", "Cupboard", "Ключ довольно странной формы.", "Icons/Key", ref booksFunctionStatus, (byte)Inventory.itemType.usedItem);
                    booksFunctionStatus++;
                }
                else
                {
                    progressStatus = (byte)progressStatusList.Ready;
                    progressBarSpeed = 1f;
                }
            }
            else
                textObject.text = rowNumber[new System.Random().Next(2, rowNumber.Length)];
        }
    }

    public void FlowerFunction()
    {
        textObject.text = rowNumber[0];
        clickedObject.GetComponent<Animator>().SetBool("startAnimation", true);
    }

    public void PhotoFunction()
    {
        textObject.text = rowNumber[0];
    }

    void FlasketFunction()
    {
        if (clickedObject.name == "Flasket")
        {
            if (flasketFunctionStatus == 0)
            {
                textObject.text = rowNumber[0];
                flasketFunctionStatus++;
            }
            else
            if (flasketFunctionStatus == 1)
            {
                if (progressStatus == (byte)progressStatusList.Complite)
                {
                    textObject.text = rowNumber[1];
                    inventory.AddItemToInventory("PieceOfCloth", "Bath", "Кусок старой ткани.", "Icons/PieceOfCloth", ref flasketFunctionStatus, (byte)Inventory.itemType.usedItem);
                    flasketFunctionStatus++;
                }
                else
                {
                    progressStatus = (byte)progressStatusList.Ready;
                    progressBarSpeed = 1f;
                }
            }
            else
            if (flasketFunctionStatus == 2)
            {
                clickedObject.name = clickedObject.name + "(disabled)";
                Destroy(clickedObject.GetComponent<Button>());
                Destroy(clickedObject.GetComponent<MoveToListener>());
                flasketFunctionStatus++;
                StopBasicFunction();
            }
        }
    }

    void BathFunction()
    {
        if (clickedObject.name == "EmptyBath")
            textObject.text = rowNumber[0];
        if (clickedObject.name == "Bath")
        {
            if (bathFunctionStatus == 0)
            { 
                if (progressStatus == (byte)progressStatusList.Complite)
                {
                    textObject.text = rowNumber[0];
                    inventory.AddItemToInventory("WetPieceOfCloth", "Oil"/*Attention*/, "Мокрый кусок старой ткани.", "Icons/WetPieceOfCloth", ref bathFunctionStatus, (byte)Inventory.itemType.usedItem);
                    bathFunctionStatus++;
                }
                else
                {
                    if (!useItemForProgress)
                        textObject.text = rowNumber[1];
                    else
                    {
                        progressStatus = (byte)progressStatusList.Waiting;
                        progressBarSpeed = 0.7f;
                    }
                }
            }
            else
            if (bathFunctionStatus == 1)
                textObject.text = rowNumber[1];
        }
    }

    void BathAnimation()
    {
        if (clickedObject.name == "Faucet")
        {
            clickedObject.transform.parent.GetComponent<Animator>().SetBool("GoFilling", true);
            clickedObject.transform.parent.name = "Bath";
            Destroy(clickedObject);
            clickedObject = null;
            StopBasicFunction();
        }
    }

    //АЛО
    void OilFunction()
    {
        if (clickedObject.name == "Oil")
        {
            textObject.text = rowNumber[0];
            if (progressStatus == (byte)progressStatusList.Complite)
            {
                textObject.text = rowNumber[1];
                Destroy(clickedObject);
            }
            else
            {
                if (useItemForProgress)
                {
                    progressStatus = (byte)progressStatusList.Waiting;
                    progressBarSpeed = 0.1f;
                }
            }
        }
        
    }

    void CloseInTheFunction()
    {
        if (clickedObject.name == "ClosetInTheBathroom")
        {
            inventory.AddItemToInventory("KeyToTheSafe", "Safe"/*Attention*/, "Странный ключ.", "Icons/Key2", ref safeFunctionStatus, (byte)Inventory.itemType.usedItem);
            textObject.text = rowNumber[0];
            Destroy(clickedObject.GetComponent<Button>());
        }
    }
    void Safe()
    {
        if (clickedObject.name == "Safe")
        {
            if (usePermitted)
                textObject.text = rowNumber[0];
            else
                textObject.text = "Без ключа, мне его не открыть.";
        }
    }
    //АЛО

    void ProgressBar()
    {
        progressBar.transform.position = new Vector3 (mainCharacter.transform.position.x, mainCharacter.transform.position.y + 3.13f, progressBar.transform.position.z);
        if (GameObject.Find("Character").GetComponent<CharacterController>().rb2D.velocity == Vector2.zero)
        {
            if (clickedObject != null && clickedObject.tag == "Executable")
            {
                if (progressStatus == (byte)progressStatusList.Ready)
                {
                    progressBar.SetActive(true);
                    progressBarValue.fillAmount += Time.deltaTime * progressBarSpeed;
                    clickOnObject = false;
                }
                else
                if (progressStatus == (byte)progressStatusList.Waiting)
                {
                    if (useItemForProgress)
                    {
                        progressBar.SetActive(true);
                        progressBarValue.fillAmount += Time.deltaTime * progressBarSpeed;
                        clickOnObject = false;
                    }
                }
                if (progressBarValue.fillAmount == 1f)
                {
                    progressBar.SetActive(false);
                    progressStatus = (byte)progressStatusList.Complite;
                    clickOnObject = true;
                    progressBarValue.fillAmount = 0f;
                    StartCoroutine(SetStatusAtNotReadyDelayed());
                }
                else
                if (progressBarValue.fillAmount > 0 && progressBarValue.fillAmount < 1)
                {
                    if (progressStatus == (byte)progressStatusList.NotReady)
                    {
                        progressBarValue.fillAmount = 0f;
                        progressBar.SetActive(false);
                        clickedObject.GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
        else
        {
            if (progressBarValue.fillAmount > 0 && progressBarValue.fillAmount < 1)
            {
                clickedObject.GetComponent<Button>().interactable = true;
                progressStatus = (byte)progressStatusList.NotReady;
                progressBar.SetActive(false);
            }
            progressBarValue.fillAmount = 0f;
        }
    }

    public void SelectUnsuitablePhrase()
    {
        int i = 0, counter = 0;
        foreach (List<KeyValuePair<string, object>> row in DatabaseService.ExecuteCommand("SELECT Phrase FROM UnsuitablePhrases;"))
            unsuitablePhrases = new string[++counter];
        foreach (List<KeyValuePair<string, object>> row in DatabaseService.ExecuteCommand("SELECT Phrase FROM UnsuitablePhrases;"))
            foreach (KeyValuePair<string, object> col in row)
                unsuitablePhrases[i++] = col.Value.ToString();
        counter = 0;
        i = 0;
    }

    void UseItemOnObject()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Container").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Container")[i].GetComponent<Toggle>().isOn)
            {
                GameObject item = GameObject.FindGameObjectsWithTag("Container")[i];
                if (item.GetComponent<Item>().suitableObject == clickedObject.name)
                {
                    if (clickedObject.tag == "Executable")
                    {
                        useItemForProgress = true;
                        if (progressStatus == (byte)progressStatusList.Complite)
                        {
                            item.GetComponent<Item>().DeactivateContainer();
                            DestroyImmediate(item);
                            usePermitted = true;
                            StartCoroutine(DisableUsePermittedDelayed());
                        }
                    }
                    else
                    {
                        item.GetComponent<Item>().DeactivateContainer();
                        DestroyImmediate(item);
                        usePermitted = true;
                        StartCoroutine(DisableUsePermittedDelayed());
                    }
                }
                else
                {
                    item.GetComponent<Item>().DeactivateContainer();
                    textObject.text = unsuitablePhrases[new System.Random().Next(0, unsuitablePhrases.Length)];
                    ShowText();
                    StopBasicFunction();
                }
            }
        }
    }

    void StopBasicFunction()
    {
        stopBasicFunction = true;
    }

    IEnumerator SetStatusAtNotReadyDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        progressStatus = (byte)progressStatusList.NotReady;
    }

    IEnumerator DisableUsePermittedDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        usePermitted = false;
    }
}
