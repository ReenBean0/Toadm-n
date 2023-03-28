using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingController : MonoBehaviour
{
    public float minDistance = 100;//Minimum distance needed to launch a leap
    public float maxDistance = 300;//Maximum distance for max force to lauch the leap
    public float forceScale = 100;//Scale to divide distance to resonable force to leap
    bool LeapMode = false, startLeap = false;
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
        //Debug.Log("Pos diff" + (posChecker - transform.position));
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
                //Debug.Log("Left click down");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //Launch
                //range between 100~300
                //Debug.Log(Vector2.Distance(mouseOriginPos, mouseCurrentPos));
                float mouseDistance = Vector2.Distance(mouseOriginPos, mouseCurrentPos);
                //start launch if drag distance reach minimum accept distance
                if (mouseDistance > minDistance)
                {
                    LeapMode = false;
                    if (mouseDistance > maxDistance)
                    {
                        //set player drag distance to max if more than acceptable
                        mouseDistance = maxDistance;
                    }
                    float dragPower = mouseDistance / forceScale;
                    Vector2 posDiff = mouseOriginPos - mouseCurrentPos;
                    //Debug.Log(posDiff);
                    GetComponent<Rigidbody2D>().AddForce(posDiff * dragPower);
                }
                //Debug.Log("Left click up");
            }
            else if (Input.GetMouseButton(0))
            {
                mouseCurrentPos = Input.mousePosition;
                //Debug.Log("Left click holding");
            }
        }
        posChecker = transform.position;
    }
}