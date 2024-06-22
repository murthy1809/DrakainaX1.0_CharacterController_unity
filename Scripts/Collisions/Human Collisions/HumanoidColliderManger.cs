//using GameDevTV.Inventories;
//using GameDevTV.UI.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidColliderManger : ColliderManager
{
    [SerializeField] internal HumanoidRayCasts humanoidRay;
    [SerializeField] Text text;
    public bool isClimbing, isClimbingExit, inventoryHit;
    public int y;
    public Vector3 ladderTransform;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ladder" && Input.GetKeyDown(KeyCode.Y))
        {
           
            isClimbing = !isClimbing;
            ladderTransform = other.transform.position;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder" && isClimbing)
        {

            isClimbingExit = true;
            isClimbing = false;
           
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Obstacle")
        {

            GetComponent<InputController>().isAction = false;
        
        }
        if (hit.collider.tag == "Inventory")
        {
            inventoryHit = true;
        }
        else
        {
            inventoryHit = false;
        }
    }

}
