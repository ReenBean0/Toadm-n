using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ToadEyeMovement : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] float rotationSpeed = 10f;
    //this can be changed to make the eyes rotate faster or slower to make it more natural

    void Update()
    {
        Vector3 target = targetObject.transform.position;
        target.z = transform.position.z;
        Debug.Log(target);   
        Vector3 direction = target - transform.position;
        Debug.Log("position at " + transform.position.x + " " + transform.position.y);

        // Calculate the angle, only works if the pupil is at the top of the eye
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the eyes
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
