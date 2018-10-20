using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Item : MonoBehaviour {

    public string suitableObject;
    public string descriptionText;
    bool _switch, _toggle = false;
    float timer = 0f;
    GameObject _descriptionPanel;

    void FixedUpdate()
    {
        OnDescriptionPanel();
        SwitchDescriptionPanel();
    }

    void Start()
    {
        _descriptionPanel = GameObject.Find("GameController").GetComponent<Inventory>().descriptionPanel;
    }

    public void SelectContainer()
    {
        if (!_switch)
        {
            if (this.GetComponent<Toggle>().isOn == true)
                this.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InventorySelectableContainer");
            else
                this.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InventoryContainer");
        }
        else
        {
            this.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InventoryContainer");
            DeactivateContainer();
        }
    }

    public void DeactivateContainer()
    {
        this.GetComponent<Toggle>().isOn = false;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().useItemForProgress = false;
    }

    public void SelectText()
    {
        if (!_switch)
        {
            foreach (List<KeyValuePair<string, object>> row in DatabaseService.ExecuteCommand("SELECT Text FROM Letters WHERE Name = '" + this.name + "';"))
                foreach (KeyValuePair<string, object> col in row)
                    GameObject.Find("GameController").GetComponent<PaperForText>().paperText.text = col.Value.ToString();
            GameObject.Find("GameController").GetComponent<PaperForText>().OpenPaperListCall();
        }
    }

    public void ButtonDown()
    {
        timer = Time.time;
        _toggle = true;
    }

    public void ButtonUp()
    {
        _toggle = false;
    }

    void OnDescriptionPanel()
    {
        if (_toggle && Time.time - timer >= 1f)
        {
            _switch = true;
            _descriptionPanel.transform.GetChild(0).GetComponent<Text>().text = descriptionText;
            if (_descriptionPanel.transform.position.y < -2.7f)
                _descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.up * Time.deltaTime * 500f;
            else
            if (GameObject.Find("GameController").GetComponent<Inventory>().inventoryOpen)
                _descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                _toggle = false;
        }
        else
        {
            if(_switch)
            {
                if (_descriptionPanel.transform.position.y > -3.3f)
                    _descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.down * Time.deltaTime * 500f;
                else
                {
                    _descriptionPanel.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    _switch = false;
                }
            }
        }
    }

    void SwitchDescriptionPanel()
    {
        if (GameObject.Find("GameController").GetComponent<Inventory>().descriptionOpen != _switch)
            GameObject.Find("GameController").GetComponent<Inventory>().descriptionOpen = _switch;
    }
}
