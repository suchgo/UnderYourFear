using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTheAnotherRoom : MonoBehaviour {

    public GameObject currentRoom;
    public bool isActive = false;
    public float characterPositionY = 0f;
    Camera mainCamera;
    GameObject character;
    ScreenEffects screenEffects;
    CharacterController characterController;

    // Use this for initialization
    void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        character = GameObject.FindGameObjectWithTag("Player");
        screenEffects = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScreenEffects>();
        characterController = character.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangeRoom();
    }

    void ChangeRoom()
    {
        if (isActive && characterController.doorIsSelected)
        {
            characterController.IsStuck();
            screenEffects.rateOfChangeValue = 0.03f;
            screenEffects.BlackoutScreen();
            if (screenEffects.isBlackoutScreen)
            {
                mainCamera.transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, mainCamera.transform.position.z);
                if (currentRoom.transform.position.x > character.transform.position.x)
                    character.transform.position = new Vector3(currentRoom.transform.position.x - 6, characterPositionY, character.transform.position.z);
                else
                    character.transform.position = new Vector3(currentRoom.transform.position.x + 6, characterPositionY, character.transform.position.z);
                screenEffects.LightingScreen();
                if (!screenEffects.isBlackoutScreen)
                {
                    isActive = false;
                    characterController.doorIsSelected = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ("Player".Equals(collision.gameObject.tag))
            isActive = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ("Player".Equals(collision.gameObject.tag))
            if (!characterController.doorIsSelected)
                isActive = false;
    }
}
