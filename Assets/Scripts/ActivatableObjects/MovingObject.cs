using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moving object that is activated by a switch or button
/// Animates to predetermined positions
/// Interact method is called by any switches or buttons that have this object as a target object
/// - Henry Paul
/// </summary>
public class MovingObject : MonoBehaviour, IActivatableObject
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Quaternion startRot;
    [SerializeField] Vector3 endPos;
    [SerializeField] Quaternion endRot;
    [SerializeField] float animSpeed;

    bool isOn;

    public void Start()
    {
        isOn = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }

    public void Interact()
    {
        if (isOn)
        {
            InteractOff();
        }
        else
        {
            InteractOn();
        }
    }

    public void InteractOff()
    {
        isOn = false;
        StartCoroutine(MoveToPosition(startPos, startRot));
    }

    public void InteractOn()
    {
        isOn = true;
        StartCoroutine(MoveToPosition(endPos, endRot));
    }

    IEnumerator MoveToPosition(Vector3 target, Quaternion angle)
    {
        float distance = Vector3.Distance(transform.position, target);
        float duration = distance / animSpeed;

        Vector3 animationStartPos = transform.position;
        Quaternion animationRotPos = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Animate object based on elapsed time
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(animationStartPos, target, t);
            transform.rotation = Quaternion.Slerp(animationRotPos, angle, t);

            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Set the final position of the object to the target position to ensure accuracy
        transform.rotation = angle;
        transform.position = target;
    }
}
