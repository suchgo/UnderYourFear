using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPhrase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.tag == "Player")
        {
            GameObject.Find("GameController").GetComponent<Game>().ShowText();
            GameObject.Find("GameController").GetComponent<Game>().textObject.text = "Моя голова... Что-то мне не по себе...";
            Destroy(gameObject);
        }
    }
}
