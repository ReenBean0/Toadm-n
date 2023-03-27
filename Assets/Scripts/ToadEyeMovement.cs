using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadEyeMovement : MonoBehaviour
{
  
    // Code given by a friend from their game, will update this to look at the fly(s) position
    
    void Update()
    {
        //Rotate the toads eyes to always face the users cursor
        Vector3 targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
