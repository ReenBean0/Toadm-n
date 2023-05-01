using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject startingBound;
    [SerializeField] GameObject toad;
    [SerializeField] float targetSpeed;
    [SerializeField] GameObject flies;
    CameraBounds currentBound;
    bool isCameraMoving;

    public Camera Camera { get { return camera; } }

    // Start is called before the first frame update
    void Start()
    {
        currentBound = startingBound.GetComponent<CameraBounds>();
        camera.transform.position = currentBound.TargetCamPos;
        camera.orthographicSize = currentBound.TargetScale;
        isCameraMoving = false;
    }

    private void Update()
    {
        if (isCameraMoving)
        {
            flies.transform.parent = camera.transform;
        }
        else
        {
            flies.transform.parent = null;
        }
    }

    /// <summary>
    /// Move and attach camera to toad
    /// </summary>
    public void FollowToad()
    {
        isCameraMoving = true;
        camera.transform.position = new Vector3(toad.transform.position.x, toad.transform.position.y, camera.transform.position.z);
        camera.transform.parent = toad.transform;
    }

    /// <summary>
    /// This will be called by the camera bounds scripts when the toad collides with them
    /// </summary>
    public void onCollideWithCameraBounds(CameraBounds cameraBounds)
    {
        if (cameraBounds != currentBound)
        {
            camera.transform.parent = null;
            currentBound = cameraBounds;
            StartCoroutine(AnimateCameraToPos(cameraBounds.TargetCamPos, cameraBounds.TargetScale));
        }
    }

    /// <summary>
    /// Animate camera to target position and orthographic size
    /// </summary>
    public IEnumerator AnimateCameraToPos(Vector3 targetPosition, float targetScale)
    {
        isCameraMoving = true;
        Debug.Log($"Move camera to: {targetPosition.x}, {targetPosition.y}, {targetPosition.z}");
        Transform camTransform = camera.GetComponent<Transform>();

        // Calculate distance and duration of animation
        float positionDistance = Vector3.Distance(camTransform.position, targetPosition);
        float positionDuration = (positionDistance / targetSpeed) * 2;

        // Calculate distance and duration of animation for scale
        float scaleDistance = Mathf.Abs(camTransform.localScale.x - targetScale);
        float scaleDuration = (scaleDistance / targetSpeed) * 2;

        float startTime = Time.time;
        float endTime = startTime + Mathf.Max(positionDuration, scaleDuration);

        while (Time.time < endTime)
        {
            // Calculate the current progression of the animation between0 and 1, with
            // 0 being the start of the animation and 1 being the end of the animation
            float t = Mathf.InverseLerp(startTime, endTime, Time.time);

            // Adjust movement speed based on animation progression
            float adjustedSpeed = Mathf.Lerp(0, targetSpeed, Mathf.SmoothStep(0, 1, t));

            // Move camera
            camTransform.position = Vector3.MoveTowards(camTransform.position, targetPosition, adjustedSpeed * Time.deltaTime);

            // Adjust orhographic size based on animation progression
            float adjustedScale = Mathf.Lerp(camera.orthographicSize, targetScale, Mathf.SmoothStep(0, 1, t));
            camera.orthographicSize = adjustedScale;

            // Pause coroutine until next frame
            yield return null;
        }

        // Animation complete
        camTransform.position = targetPosition;
        camera.orthographicSize = targetScale;
        isCameraMoving = false;
    }

    /// <summary>
    /// Animate camera to a position, pause for 'x' seconds and animate to previous position. Use with "StartCoroutine"
    /// </summary>
    public IEnumerator AnimateCameraEvent(Vector3 targetPosition, float targetScale, float pauseDuration, float startDelay)
    {
        Vector3 previousCamPos = camera.transform.position;
        float previousCamScale = camera.orthographicSize;

        // Delay before camera movement
        yield return new WaitForSecondsRealtime(startDelay);

        // Initial camera movement
        yield return StartCoroutine(AnimateCameraToPos(targetPosition, targetScale));
        
        // Pause
        yield return new WaitForSecondsRealtime(pauseDuration);

        // Return to previous position
        yield return StartCoroutine(AnimateCameraToPos(previousCamPos, previousCamScale));
    }
}
