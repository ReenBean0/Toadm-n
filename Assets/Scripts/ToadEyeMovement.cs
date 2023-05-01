using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadEyeMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    //this can be changed to make the eyes rotate faster or slower to make it more natural

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        
        Vector3 direction = mousePos - transform.position;

        // Calculate the angle, only works if the pupil is at the top of the eye
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the eyes
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
