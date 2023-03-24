using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingController : MonoBehaviour
{
    Vector2 mouseOriginPos, mouseCurrentPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseOriginPos = Input.mousePosition;
            Debug.Log("Left click down");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Launch
            Debug.Log("Left click up");
        }
        else if (Input.GetMouseButton(0))
        {
            mouseCurrentPos = Input.mousePosition;
            Debug.Log("Left click holding");
        }
    }
}
