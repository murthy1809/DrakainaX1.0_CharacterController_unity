using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class GroundAligner : MonoBehaviour
{
    RaycastHit raycast;
    [SerializeField] GameObject Origin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Origin.transform.position, Vector3.down * 10, Color.blue);
        Ray Line = new Ray(Origin.transform.position, Vector3.down);
        Physics.Raycast(Line, out raycast, 10);
        Origin.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycast.normal);
    }
}
