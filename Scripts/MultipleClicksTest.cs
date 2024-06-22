using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleClicksTest : MonoBehaviour
{ 
//The amount of seconds the code will wait between each click
public float ClickDelay = 0.2f;

//How many click are currently registers
public int _currClicks;

//The current amount of time that hase passed since the last click
public float _clickTime;

// Update is called once per frame
void Update()
{

    CheckForClicks();
}

private void CheckForClicks()
{
    //If we detected a new click, we increase _currClick counter, and reset the time since last click (_clickTime)
    //Note that his will not work on GetMouseButtonDown
    if (Input.GetMouseButtonUp(0))
    {
        _currClicks++;

        //_click time is resetted at every click as we want the amount of time since LAST click, not since the first click
        _clickTime = 0;
    }

    //If there were no other clicks, stop all computations
    if (_currClicks == 0) return;

    //As long as the current time since last click, is smaller than the amount of time
    //we want our code to wait for a click, we junt increase the _clickTime timer
    if (_clickTime < ClickDelay)
    {
        _clickTime += Time.deltaTime;
        return;
    }

    //If the amount of seconds we want to wait has passed, we handel the curr amount of cliks and reset the values
    HandleClicks(_currClicks);
    _currClicks = 0;
    _clickTime = 0;

}

private void HandleClicks(int amountOfClicks)
{
    //we hande each amount of clicks
    switch (amountOfClicks)
    {
        case 1:
            Debug.Log("Single Click");
            break;
        case 2:
            Debug.Log("Double Click");
            break;
        case 3:
            Debug.Log("Triple Click");
            break;
        case 4:
            Debug.Log("Fourth Click");
            break;
        default:
            Debug.Log("You've cliked: " + amountOfClicks + " times");
            break;
    }
}
}
