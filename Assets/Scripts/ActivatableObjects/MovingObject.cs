using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour, IActivatableObject
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] float animSpeed;

    public void Start()
    {
        transform.position = startPos;
    }

    public void InteractOff()
    {
        StartCoroutine(MoveToPosition(startPos));
    }

    public void InteractOn()
    {
        StartCoroutine(MoveToPosition(endPos));
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        float duration = distance / animSpeed;

        Vector3 animationStartPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Animate object based on elapsed time
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(animationStartPos, target, t);

            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Set the final position of the object to the target position to ensure accuracy
        transform.position = target;
    }
}
