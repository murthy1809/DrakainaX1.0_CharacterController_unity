using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour
{
    [SerializeField] float ClickDelay = 0.2f;
    [SerializeField] bool LoadArrow;
    int _currClicks;
    float _clickTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // CheckForClicks("PrimaryAttack", 0.5f);
        if (Input.GetButtonDown("PrimaryAttack"))
        {
            Debug.Log("LoadArrow");
            LoadArrow = true;
        }
        if (LoadArrow )
        {
            if (Input.GetButton("PrimaryAttack"))
            {
                Debug.Log("DrawArrow");
            }          

        }

        if (Input.GetButtonUp("PrimaryAttack"))
        {
            Debug.Log("ShootArrow");
            LoadArrow = false;
        }
    }

    private void CheckForClicks(string buttonName, float clickDelay)
    {

        if (Input.GetButtonDown(buttonName))
        {
            _currClicks++;
            _clickTime = 0;
            
        }

        if (_currClicks == 0) return;


        if (_clickTime < ClickDelay)
        {
            _clickTime += Time.deltaTime;
            return;
        }


        HandleClicks(_currClicks);
        _currClicks = 0;
        _clickTime = 0;

    }

    private void HandleClicks(int amountOfClicks)
    {
        if (amountOfClicks == 1)
        {
            Debug.Log("single");
        }
        if (amountOfClicks == 2)
        {
            Debug.Log("double");
        }
    }
   
}
