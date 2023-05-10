using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// - Joshua Booth
/// </summary>
public class EyeMovement : MonoBehaviour
{
    [SerializeField] bool isFly = false;
    [SerializeField] GameObject targetObject;
    [SerializeField] float rotationSpeed = 10f;
    //this can be changed to make the eyes rotate faster or slower to make it more natural

    private void Start()
    {
        if(!isFly)
        {
            targetObject = GetComponentInParent<TongueController>().flyCursor;
        }    
    }

    void Update()
    {
        Vector3 target;
        if (isFly)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            target = targetObject.transform.position;
        }
        target.z = transform.position.z;
        Vector3 direction = target - transform.position;

        // Calculate the angle, only works if the pupil is at the top of the eye
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the eyes
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
