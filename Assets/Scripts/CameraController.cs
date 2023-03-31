using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] List<GameObject> cameraBounds = new List<GameObject>();
    [SerializeField] GameObject toad;

    Transform toadTransform;
    List<BoxCollider2D> colliders = new List<BoxCollider2D>();

    // Start is called before the first frame update
    void Start()
    {
        toadTransform = toad.GetComponent<Transform>();
        foreach (GameObject cameraBound in cameraBounds)
        {
            colliders.Add(cameraBound.GetComponent<BoxCollider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // This will be called by the camera bounds scripts
    void onCollideWithCameraBounds(GameObject cameraBounds)
    {

    }
}
