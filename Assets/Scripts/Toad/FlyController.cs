using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed; //degrees per second
    [SerializeField] private float moveSpeed;

    public bool lockMovement = false;

    // Update is called once per frame
    void Update()
    {
        //constant rotation of flies on the z axis
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (!lockMovement)
        {
            //Horizontal axis = Left and Right arrow keys + A key and D key
            float xMovement = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            //Vertical axis = Up and Down arrow keys + W key and S key
            float yMovement = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            //moves the flies
            transform.Translate(xMovement, yMovement, 0, Space.World);
        }
    }
}
