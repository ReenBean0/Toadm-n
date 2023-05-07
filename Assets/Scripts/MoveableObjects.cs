using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    public GameObject toad;
    bool trigger = false;
    float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            //Horizontal axis = Left and Right arrow keys + A key and D key
            float xMovement = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            //Vertical axis = Up and Down arrow keys + W key and S key
            float yMovement = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            //moves the flies
            transform.Translate(xMovement, yMovement, 0, Space.World);
            //toad.GetComponent<TongueController>().ChangeTonguePosition(xMovement, yMovement);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //toad.GetComponent<TongueController>().FinishMoveableObject();
                trigger = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Tongue"))
        {
            //toad.GetComponent<TongueController>().TriggerMoveableObject();
            trigger = true;
        }
    }
}
