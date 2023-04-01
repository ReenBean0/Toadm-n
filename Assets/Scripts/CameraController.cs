using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] List<GameObject> cameraBounds = new List<GameObject>();
    [SerializeField] GameObject toad;
    [SerializeField] float targetSpeed;

    CameraBounds currentBound;

    // Start is called before the first frame update
    void Start()
    {
        currentBound = cameraBounds[0].GetComponent<CameraBounds>();
    }

    // This will be called by the camera bounds scripts when the toad collides with them
    public void onCollideWithCameraBounds(CameraBounds cameraBounds)
    {
        if (cameraBounds != currentBound)
        {
            currentBound = cameraBounds;
            StartCoroutine(AnimateCameraToPos(cameraBounds.TargetCamPos));
        }
    }

    IEnumerator AnimateCameraToPos(Vector3 targetPosition)
    {
        Debug.Log($"Move camera to: {targetPosition.x}, {targetPosition.y}, {targetPosition.z}");
        Transform camTransform = camera.GetComponent<Transform>();

        // Calculate distance and duration of animation
        float distance = Vector3.Distance(camTransform.position, targetPosition);
        float duration = (distance / targetSpeed) * 2;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            // Calculate the current progression of the animation between0 and 1, with
            // 0 being the start of the animation and 1 being the end of the animation
            float t = Mathf.InverseLerp(startTime, endTime, Time.time);

            // Adjust movement speed based on animation progression
            float adjustedSpeed = Mathf.Lerp(0, targetSpeed, Mathf.SmoothStep(0, 1, t));

            // Move camera
            camTransform.position = Vector3.MoveTowards(camTransform.position, targetPosition, adjustedSpeed * Time.deltaTime);

            // Pause coroutine until next frame
            yield return null;
        }

        // Animation complete
        camTransform.position = targetPosition;
    }
}
