using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] float targetScale = 5;
    [SerializeField] Vector3 targetCamPos = new Vector3(0, 0, 0);
    [SerializeField] GameObject gameManagerObject;

    CameraController cameraController;

    void Start()
    {
        cameraController = gameManagerObject.GetComponent<CameraController>();
    }

    public Vector3 TargetCamPos
    {
        get { return targetCamPos; }
    }

    public float TargetScale
    {
        get { return targetScale; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cameraController.onCollideWithCameraBounds(this);
        }
    }
}
