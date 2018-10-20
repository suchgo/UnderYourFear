using UnityEngine;

public class MoveToListener : MonoBehaviour
{
    bool pressure;
    Vector3 pressurePosition;

    void OnMouseDown()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            pressure = Input.GetTouch(0).phase == TouchPhase.Began;
            pressurePosition = Input.GetTouch(0).position;
        }
        else
        {
            pressure = Input.GetMouseButtonDown(0);
            pressurePosition = Input.mousePosition;
        }
        if (pressure)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(pressurePosition));
            if (rayHit.transform != null)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().MoveTo(rayHit.point);
                if (rayHit.collider.tag == "Floor" || rayHit.collider.tag == "Door")
                {
                    GameObject.Find("GameController").GetComponent<Game>().clickOnObject = false;
                    GameObject.Find("GameController").GetComponent<Game>().progressStatus = (byte)Game.progressStatusList.NotReady;
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Container").Length; i++)
                        GameObject.FindGameObjectsWithTag("Container")[i].GetComponent<Item>().DeactivateContainer();
                }
                if (rayHit.collider.tag == "Door")
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().doorIsSelected = true;
                else
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().doorIsSelected = false;
            }
        }
    }
}
