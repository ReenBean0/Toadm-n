using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] GameObject flyCursor;
    [SerializeField] GameObject tongue;
    
    Transform flyTransform;
    float flyX;
    float flyY;

    float toadX;
    float toadY;

    // Start is called before the first frame update
    void Start()
    {
       flyTransform = flyCursor.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flyX = flyTransform.position.x;
            flyY = flyTransform.position.y;
            Debug.Log($"Launch tongue at X={flyX}, y={flyY}");

            // Spawn tongue
            toadX = transform.position.x;
            toadY = transform.position.y;
            GameObject tongueObject = Instantiate(tongue, new Vector3(flyX, flyY, 0), Quaternion.identity);

            // Scale tongue towards point
            //tongueObject.transform.LookAt(GetComponent<Transform>());
            //Debug.Log($"Up vector: {transform.forward}");
        }
    }
}
