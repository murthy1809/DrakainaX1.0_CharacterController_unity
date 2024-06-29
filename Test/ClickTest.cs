using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    private float clickTime = 0.0f;
    private float clickDelay = 0.2f;
    private int clickCount = 0;

    void Update()
    {
        // Detect mouse button pressed down
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
            clickTime = Time.time;
            Debug.Log("Mouse button pressed down");
        }

        // Detect mouse button held down
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse button is being held down");
        }

        // Detect mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse button released");
            if (Time.time - clickTime < clickDelay)
            {
                Invoke("CheckClicks", clickDelay);
            }
        }
    }

    void CheckClicks()
    {
        if (clickCount == 1)
        {
            Debug.Log("Single click detected");
        }
        else if (clickCount == 2)
        {
            Debug.Log("Double click detected");
        }
        else if (clickCount == 3)
        {
            Debug.Log("Triple click detected");
        }
        clickCount = 0;
    }
}
