using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.AnnotationUtility;
using UnityEngine.UIElements;

/* Written by Rian
 * A reworking of the code found in Henry's interactable devices
 * This rework aims to simplify that code, using abstraction to 
 * make it more readable and easier to build levels with.
 */

public abstract class ObjectResponse : Interactable
{
    [SerializeField] protected float animSpeed = 5;

    protected virtual IEnumerator MoveToPosition(Vector3 target, Quaternion angle, bool disableAtEnd = false)
    {
        float distance = Vector3.Distance(transform.localPosition, target);
        float duration = distance / animSpeed;

        Vector3 animationStartPos = transform.localPosition;
        Quaternion animationRotPos = transform.localRotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Animate object based on elapsed time
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(animationStartPos, target, t);
            transform.localRotation = Quaternion.Slerp(animationRotPos, angle, t);

            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Set the final position of the object to the target position to ensure accuracy
        transform.localRotation = angle;
        transform.localPosition = target;
        if (disableAtEnd) isActive = false;
    }
}
