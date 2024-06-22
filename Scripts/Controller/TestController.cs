using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3 (transform.position.x,transform.position.y+5,transform.position.z);
        }
        Debug.Log("move");
    }
}
