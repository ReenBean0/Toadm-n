using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed; //degrees per second
    [SerializeField] private float moveSpeed;
    public CircleCollider2D radius;



    // Update is called once per frame
    void Update()
    {
        //constant rotation of flies on the z axis
        this.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);


        float xMovement = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float yMovement = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        this.transform.Translate(xMovement, yMovement, 0, Space.World);

      //Physics2D.OverlapCollider(radius,)

    }
}
