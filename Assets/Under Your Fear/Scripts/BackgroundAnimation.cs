using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour {

    public GameObject CloudsLeft1, CloudsLeft2, CloudsLeft3, CloudsRight1, CloudsRight2, CloudsRight3;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Clouds();
    }

    void Clouds()
    {
        CloudsLeft1.GetComponent<Rigidbody2D>().velocity = Vector2.left * Time.deltaTime * 10f;
        if (CloudsLeft1.transform.position.x <= -5.1f)
            CloudsLeft1.transform.position = new Vector3(-1f, CloudsLeft1.transform.position.y, CloudsLeft1.transform.position.z);

        CloudsLeft2.GetComponent<Rigidbody2D>().velocity = Vector2.left * Time.deltaTime * 20f;
        if (CloudsLeft2.transform.position.x <= -4.33f)
            CloudsLeft2.transform.position = new Vector3(-1.75f, CloudsLeft2.transform.position.y, CloudsLeft2.transform.position.z);

        CloudsLeft3.GetComponent<Rigidbody2D>().velocity = Vector2.left * Time.deltaTime * 15f;
        if (CloudsLeft3.transform.position.x <= -4.96f)
            CloudsLeft3.transform.position = new Vector3(-1.12f, CloudsLeft3.transform.position.y, CloudsLeft3.transform.position.z);

        CloudsRight1.GetComponent<Rigidbody2D>().velocity = Vector2.left * Time.deltaTime * 10f;
        if (CloudsRight1.transform.position.x <= 0.415f)
            CloudsRight1.transform.position = new Vector3(4.5f, CloudsRight1.transform.position.y, CloudsRight1.transform.position.z);

        CloudsRight2.GetComponent<Rigidbody2D>().velocity = Vector2.left * Time.deltaTime * 20f;
        if (CloudsRight2.transform.position.x <= 1.16f)
            CloudsRight2.transform.position = new Vector3(3.75f, CloudsRight2.transform.position.y, CloudsRight2.transform.position.z);

        CloudsRight3.GetComponent<Rigidbody2D>().velocity = Vector2.left * Time.deltaTime * 15f;
        if (CloudsRight3.transform.position.x <= 0.54f)
            CloudsRight3.transform.position = new Vector3(4.38f, CloudsRight3.transform.position.y, CloudsRight3.transform.position.z);
    }
}
