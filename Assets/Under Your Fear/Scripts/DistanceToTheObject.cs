using UnityEngine;

public class DistanceToTheObject : MonoBehaviour {

    public float distance = 0.5f;
    Game game;

    void Start()
    {
        game = GetComponent<Game>();
    }

    public void SetDistanceToTheObkect()
    {
        if (game.clickedObject != null)
        {
            if (game.clickedObject.name == "Cupboard")
                distance = 2.5f;
            else
            if (game.clickedObject.name == "Flower")
                    distance = 0.8f;
            else
            if (game.clickedObject.name == "Books")
                    distance = 0.8f;
            else
            if (game.clickedObject.name == "Candle")
                    distance = 3f;
            else
            if (game.clickedObject.name == "Photo")
                    distance = 3.5f;
            else
            if (game.clickedObject.name == "Flasket")
                    distance = 1.5f;
            else
            if (game.clickedObject.name == "Bath")
                    distance = 3.5f;
            else
            if (game.clickedObject.name == "EmptyBath")
                distance = 3.5f;
            else
            if (game.clickedObject.name == "Faucet")
                distance = 0.8f;
            else
            if (game.clickedObject.name == "Oil")
                distance = 1f;
            else
            if (game.clickedObject.name == "ClosetInTheBathroom")
                distance = 2.5f;
            else
            if (game.clickedObject.name == "Safe")
                distance = 1f;
        }
    }
}
