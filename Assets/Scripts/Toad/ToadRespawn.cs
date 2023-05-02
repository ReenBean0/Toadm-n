using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadRespawn : MonoBehaviour
{
    Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //reset position to before leap if fall below platform
        if (transform.position.y < -5)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = originPos;
        }
    }
    public void SavePosition(Vector3 pos)
    {
        originPos = pos;
    }
}
