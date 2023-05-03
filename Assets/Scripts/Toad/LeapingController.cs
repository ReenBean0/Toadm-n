using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeapingController : MonoBehaviour
{
    [SerializeField] GameObject leftGroundCheck;
    [SerializeField] GameObject rightGroundCheck;

    float minDistance = 1f;//Minimum distance needed to launch a leap
    float maxDistance = 2f;//Maximum distance for max force to lauch the leap
    float forceScale = 50;//Scale to multiply distance to resonable force to leap
    bool LeapMode = false, startLeap = false;
    Rigidbody2D toadRigi;
    Vector2 mouseOriginPos, mouseCurrentPos;
    Vector3 posChecker;
    LineRenderer lineRender;
    GameObject playerInputLine;

    // Start is called before the first frame update
    void Start()
    {
        posChecker = transform.position;
        #region create player line
        playerInputLine = new GameObject("playerInputLine");
        playerInputLine.transform.parent = transform;
        playerInputLine.AddComponent<LineRenderer>();
        lineRender = playerInputLine.GetComponent<LineRenderer>();
        lineRender.startWidth = 0.2f;
        lineRender.endWidth = 0.8f;
        lineRender.SetPosition(0, Vector3.zero);
        lineRender.SetPosition(1, Vector3.zero);
        lineRender.material = new Material(Shader.Find("Sprites/Default"));
        lineRender.startColor = new Color(255, 226, 0);
        lineRender.endColor = new Color(255, 0, 0);
        #endregion
        //init rigibody value
        toadRigi = GetComponent<Rigidbody2D>();
        toadRigi.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnMouseDown()
    {
        //if player press the toadman
        if (!startLeap)
        {
            GetComponent<ToadRespawn>().SavePosition(transform.position);
            LeapMode = true;
        }
    }
    void FixedUpdate()
    {
        //set the max power of leap can do
        float maxSpeed = 10;
        if (toadRigi.velocity.magnitude > maxSpeed)
        {
            toadRigi.velocity = toadRigi.velocity.normalized * maxSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftGroundCheck.transform.position, -Vector2.up, Mathf.Infinity, 1 << 7);
        RaycastHit2D rightHit = Physics2D.Raycast(rightGroundCheck.transform.position, -Vector2.up, Mathf.Infinity, 1 << 7);
        float leftDistance = 1;
        float rightDistance = 1;
        // If it hits something...
        if (leftHit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            leftDistance = Mathf.Abs(leftHit.point.y - transform.position.y);

        }
        if (rightHit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            rightDistance = Mathf.Abs(rightHit.point.y - transform.position.y);
        }

        if (leftDistance < 1 || rightDistance < 1)
        {
            // Toad is on floor
            if (startLeap == true)
            {
                // If toad was previously not on floor
                GetComponent<ToadSFXController>().PlayFloorHit();
            }
            startLeap = false;
        }
        else
        {
            startLeap = true;
        }

        //Debug.Log("Pos diff" + (posChecker - transform.position));
        //if the object stop moving, enable checker for next leap
        //    if (posChecker == transform.position)
        //{
        //    startLeap = false;
        //}
        //else
        //{
        //    startLeap = true;
        //}
        //if player press the toadman
        if (LeapMode)
        {
            #region left click down
            if (Input.GetMouseButtonDown(0))
            {
                //mouseOriginPos = Input.mousePosition;
                mouseOriginPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("Left click down");
            }
            #endregion
            #region left click up
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
                    //Debug.Log("mouseDistance="+ mouseDistance);
                    float dragPower = mouseDistance * forceScale;
                    Vector3 posDiff = mouseOriginPos - mouseCurrentPos;
                    //Debug.Log(posDiff);
                    toadRigi.AddForce(posDiff * dragPower);
                }
                lineRender.SetPosition(0,Vector3.zero);
                lineRender.SetPosition(1, Vector3.zero);
                //Debug.Log("Left click up");
            }
            #endregion
            #region holding left click
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
            #endregion
        }
        //update position to check if it is moving
        posChecker = transform.position;
    }
}