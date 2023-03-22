using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed; //degrees per second

    private void Start()
    {
        rotateSpeed = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        //constant rotation of flies on the z axis
        this.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        //captures mouse position
        Vector3 cursorPosition = Input.mousePosition;
        //translate mouse position to game world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        this.transform.position = worldPosition;
    }
}
