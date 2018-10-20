using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject inventoryGameObject, containerUsedItem, containerTextItem, descriptionPanel;
    public Button inventoryCall;
    public bool inventoryOpen = false, descriptionOpen;
    public enum itemType { usedItem, textItem };
    byte counter = 0;
    GameObject container;
    Game game;

    // Use this for initialization
    void Start () {
        game = GetComponent<Game>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        OpenAndCloseInventory();
    }

    public void CallInventory()
    {
        if (inventoryOpen)
        {
            inventoryOpen = false;
            inventoryCall.interactable = false;
        }
        else
        {
            inventoryOpen = true;
            inventoryCall.interactable = false;
        }
    }

    void OpenAndCloseInventory()
    {
        if (inventoryOpen)
        {
            if (inventoryGameObject.transform.position.y < -4.05f)
            {
                inventoryGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * Time.deltaTime * 300f;
                descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.up * Time.deltaTime * 300f;
            } 
            else
            {
                inventoryGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                inventoryCall.interactable = true;
            }
        }
        else
        {
            if(!descriptionOpen)
            {
                if (inventoryGameObject.transform.position.y > -6f)
                {
                    inventoryGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * Time.deltaTime * 300f;
                    descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.down * Time.deltaTime * 300f;
                }
                else
                {
                    inventoryGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    inventoryCall.interactable = true;
                }
            }
        }
    }

    public void AddItemToInventory(string itemName, string suitableObject, string description, string icon, ref byte objectStatus, byte _itemType)
    {
        counter = 0;
        for (int i = 0; i < inventoryGameObject.transform.childCount; i++)
        {
            if (inventoryGameObject.transform.GetChild(i).childCount == 0)
            {
                if (_itemType == (byte)itemType.usedItem)
                {
                    container = Instantiate(containerUsedItem);
                    SetStandardParametersOfContainer(itemName, i, description, icon);
                    container.GetComponent<Toggle>().group = inventoryGameObject.GetComponent<ToggleGroup>();
                    container.GetComponent<Item>().suitableObject = suitableObject;
                }
                else
                {
                    container = Instantiate(containerTextItem);
                    SetStandardParametersOfContainer(itemName, i, description, icon);
                }
                break;
            }
            else
                FullInventory(ref objectStatus);
        }
    }

    void SetStandardParametersOfContainer(string _itemName, int serialNumber, string _description, string _icon)
    {
        container.transform.SetParent(inventoryGameObject.transform.GetChild(serialNumber).transform);
        container.transform.localScale = new Vector3(1, 1, 1);
        container.GetComponent<Image>().sprite = Resources.Load<Sprite>(_icon);
        container.name = _itemName;
        container.GetComponent<Item>().descriptionText = _description;
    }

    public void FullInventory(ref byte status)
    {
        if (counter == 5)
        {
            game.textObject.text = "Мне больше не унести.";
            game.ShowText();
            status--;
        }
        else
            counter++;
    }
}
