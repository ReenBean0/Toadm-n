using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeapingController : MonoBehaviour
{
    float minDistance = 100;//Minimum distance needed to launch a leap
    float maxDistance = 300;//Maximum distance for max force to lauch the leap
    float forceScale = 200;//Scale to divide distance to resonable force to leap
    bool LeapMode = false, startLeap = false;
    Vector2 mouseOriginPos, mouseCurrentPos;
    Vector3 posChecker, originPos;
    LineRender lineRender;
    GameObject playerInputLine;
    // Start is called before the first frame update
    void Start()
    {
        posChecker = transform.position;
        originPos= transform.position;
        playerInputLine = transform.GetChild(0).gameObject;
        lineRender = playerInputLine.GetComponent<LineRender>();
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
            originPos= transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                mouseOriginPos = Input.mousePosition;
                //mouseOriginPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
                    Vector3 posDiff = mouseOriginPos - mouseCurrentPos;
                    //Debug.Log(posDiff);
                    GetComponent<Rigidbody>().AddForce(posDiff * dragPower);
                }
                //Debug.Log("Left click up");
            }
            else if (Input.GetMouseButton(0))
            {
                mouseCurrentPos = Input.mousePosition;
                /*
                mouseCurrentPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRender.SetPosition(0, new Vector3(mouseOriginPos.x,mouseOriginPos.y,0));
                lineRender.SetPosition(1, new Vector3(mouseCurrentPos.x,mouseCurrentPos.y,0));
                */
                //Debug.Log("Left click holding");
            }
        }
        if (transform.position.y < -5)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = originPos;
        }
        posChecker = transform.position;
    }
}