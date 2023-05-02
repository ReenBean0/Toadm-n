using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRotate : MonoBehaviour
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
    }
}
