using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeapingController : MonoBehaviour
{
    float minDistance = 1f;//Minimum distance needed to launch a leap
    float maxDistance = 2f;//Maximum distance for max force to lauch the leap
    float forceScale = 50;//Scale to multiply distance to resonable force to leap
    bool LeapMode = false, startLeap = false;
    Vector2 mouseOriginPos, mouseCurrentPos;
    Vector3 posChecker, originPos;
    LineRenderer lineRender;
    GameObject playerInputLine;
    // Start is called before the first frame update
    void Start()
    {
        posChecker = transform.position;
        originPos= transform.position;
        playerInputLine = transform.GetChild(0).gameObject;
        lineRender = playerInputLine.GetComponent<LineRenderer>();
        lineRender.SetPosition(0, Vector3.zero);
        lineRender.SetPosition(1, Vector3.zero);
        //init rigibody value
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        //GetComponent<Rigidbody>().freezeRotation = true;
    }
    private void OnMouseDown()
    {
        //if player press the toadman
        if (!startLeap)
        {
            originPos = transform.position;
            LeapMode = true;
        }
    }
    void FixedUpdate()
    {
        //set the max power of leap can do
        float maxSpeed = 10;
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Pos diff" + (posChecker - transform.position));
        //if the object stop moving, enable checker for next leap
        if (posChecker == transform.position)
        {
            startLeap = false;
        }
        else
        {
            startLeap = true;
        }
        //if player press the toadman
        if (LeapMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //mouseOriginPos = Input.mousePosition;
                mouseOriginPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("Left click down");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //Launch
                //range between 100~300
                Debug.Log(Vector2.Distance(mouseOriginPos, mouseCurrentPos));
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
                    Debug.Log("mouseDistance="+ mouseDistance);
                    float dragPower = mouseDistance * forceScale;
                    Vector3 posDiff = mouseOriginPos - mouseCurrentPos;
                    //Debug.Log(posDiff);
                    GetComponent<Rigidbody2D>().AddForce(posDiff * dragPower);
                }
                //Debug.Log("Left click up");
            }
            else if (Input.GetMouseButton(0))
            {
                //mouseCurrentPos = Input.mousePosition;
                
                mouseCurrentPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float maxDist = 5;
                Vector2 dir = mouseCurrentPos - mouseOriginPos;
                float dist = Mathf.Clamp(Vector2.Distance(mouseOriginPos, mouseCurrentPos), 0, maxDist);
                Vector2 newPos = mouseOriginPos + (dir.normalized * dist);
                lineRender.SetPosition(0, new Vector3(mouseOriginPos.x,mouseOriginPos.y,0));
                lineRender.SetPosition(1, new Vector3(newPos.x,newPos.y,0));

                //Debug.Log("Left click holding");
            }
        }
        //reset position to before leap if fall below platform
        if (transform.position.y < -5)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = originPos;
        }
        //update position to check if it is moving
        posChecker = transform.position;
    }
}