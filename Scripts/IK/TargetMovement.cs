using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
   [SerializeField] float speed;
    private Vector2 lookInput, screenCenter, mouseDistance;

    void Start()
    {
        //screenCenter.x = Screen.width/2;
        //screenCenter.y = Screen.height/2;
        ////Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //lookInput.x = Input.mousePosition.x;
        //lookInput.y = Input.mousePosition.y;

        //mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        //mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        //mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1);

        //Debug.Log(mouseDistance.x);

        ////transform.Translate(Vector3.right*mouseDistance.x*Time.deltaTime*speed);
    }
}
