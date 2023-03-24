using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingController : MonoBehaviour
{
    float leapingSpeed = 0.005f;
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
            StartCoroutine(Launch());
            Debug.Log("Left click up");
        }
        else if (Input.GetMouseButton(0))
        {
            mouseCurrentPos = Input.mousePosition;
            Debug.Log("Left click holding");
        }
    }
    IEnumerator Launch()
    {
        Vector3 originScale = transform.localScale;
        Vector3 newScale = originScale;
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
            Vector3 posDiff = mouseOriginPos - mouseCurrentPos;
            Vector3 newPos = transform.position + posDiff / 50;
            //X value start from 100
            //Y value start from 50
            //Debug.Log(posDiff);
            if (posDiff.x >= 0)
            {
                //to right
                transform.position = newPos;
            }
            else if (posDiff.x < 0)
            {
                //to left
            }
            if (posDiff.y >= 0)
            {
                //to up
            }
            else if (posDiff.y < 0)
            {
                //to down
            }
        }
        yield return null;
    }
}