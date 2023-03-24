using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingController : MonoBehaviour
{
    bool LeapMode = false, startLeap = false;
    float leapingSpeed = 0.005f;
    Vector2 mouseOriginPos, mouseCurrentPos;
    Vector3 posChecker;
    // Start is called before the first frame update
    void Start()
    {
        posChecker = transform.position;
    }
    private void OnMouseDown()
    {
        //if player press the toadman
        if (!startLeap)
        {
            LeapMode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pos diff" + (posChecker - transform.position));
        if (posChecker == transform.position)
        {
            startLeap = false;
        }
        else
        {
            startLeap = true;
        }
        if (LeapMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseOriginPos = Input.mousePosition;
                Debug.Log("Left click down");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LeapMode = false;
                //Launch
                //range between 100~300
                //Debug.Log(Vector2.Distance(mouseOriginPos, mouseCurrentPos));
                float mouseDistance = Vector2.Distance(mouseOriginPos, mouseCurrentPos);
                //start launch if drag distance reach 100
                if (mouseDistance > 100)
                {
                    if (mouseDistance > 300)
                    {
                        //biggest distance of drag set to 300 distance
                        mouseDistance = 300;
                    }
                    float dragPower = mouseDistance / 100;
                    Vector3 posDiff = mouseOriginPos - mouseCurrentPos;
                    //Debug.Log(posDiff);
                    GetComponent<Rigidbody>().AddForce(posDiff * dragPower);
                }
                Debug.Log("Left click up");
            }
            else if (Input.GetMouseButton(0))
            {
                mouseCurrentPos = Input.mousePosition;
                Debug.Log("Left click holding");
            }
        }
        posChecker = transform.position;
    }
}