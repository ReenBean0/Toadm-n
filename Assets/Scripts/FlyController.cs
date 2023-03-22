using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed; //degrees per second

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //constant rotation of flies on the z axis
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
