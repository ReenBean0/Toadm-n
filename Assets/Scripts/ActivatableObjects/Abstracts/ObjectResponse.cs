using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/* Written by Rian
 * A reworking of the code found in Henry's interactable devices
 * This rework aims to simplify that code, using abstraction to 
 * make it more readable and easier to build levels with.
 */

public abstract class ObjectResponse : Interactable
{
    [SerializeField] protected float animSpeed = 5;

    protected virtual IEnumerator MoveToPosition(Vector3 target, Quaternion angle, Vector3 scale, bool disableAtEnd = false)
    {
        float distance = Vector3.Distance(transform.localPosition, target) + Vector3.Distance(transform.localScale, scale);
        float duration = distance / animSpeed;

        Vector3 animationStartPos = transform.localPosition;
        Quaternion animationStartRot = transform.localRotation;
        Vector3 animationStartScale = transform.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Animate object based on elapsed time
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(animationStartScale, scale, t);
            transform.localRotation = Quaternion.Slerp(animationStartRot, angle, t);
            transform.localPosition = Vector3.Lerp(animationStartPos, target, t);

            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Set the final position of the object to the target position to ensure accuracy
        transform.localScale = scale;
        transform.localRotation = angle;
        transform.localPosition = target;
        if (disableAtEnd) isActive = false;
    }
}
