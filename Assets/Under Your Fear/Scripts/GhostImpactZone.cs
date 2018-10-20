using UnityEngine;

public class GhostImpactZone : MonoBehaviour {

    public int _mindDamage;
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
            GameObject.Find("Character").GetComponent<CharacterController>().ghostIsActive = true;
            GameObject.Find("Character").GetComponent<CharacterController>().mindDamage = _mindDamage;
        }
    }

    void OnTriggerExit2D(Collider2D _collider)
    {
        if (_collider.tag == "Player")
        {
            GameObject.Find("Character").GetComponent<CharacterController>().ghostIsActive = false;
        }
    }
}
