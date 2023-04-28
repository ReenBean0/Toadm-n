using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I stole most of this from Henry, but I needed a special type of moving platform
/// Uses anchors to move between states
/// - Rian
/// </summary>
public class AnchoredObject : MonoBehaviour
{
    [SerializeField] MovementAnchor leftAnchor;
    [SerializeField] MovementAnchor rightAnchor;
    [SerializeField] MovementAnchor startAnchor;
    [SerializeField] float animSpeed;

    Vector3 startPos => startAnchor.transform.position;
    Vector3 leftPos => leftAnchor.transform.position;
    Vector3 rightPos => rightAnchor.transform.position;

    public enum STATE
    {
        LEFT,MIDDLE,RIGHT
    }

    public STATE platformPosition = STATE.MIDDLE;

    public bool isActive;

    public void Start()
    {
        isActive = false;
        transform.position = startPos;
    }

    public void InteractOff()
    {
        isActive = false;
    }

    public void InteractOn(STATE state)
    {
        isActive = true;
        if (platformPosition == STATE.MIDDLE)
        {
            if (state == STATE.LEFT)
            {
                platformPosition = STATE.LEFT;
                StartCoroutine(MoveToPosition(leftPos));
            }
            else if (state == STATE.RIGHT)
            {
                platformPosition = STATE.RIGHT;
                StartCoroutine(MoveToPosition(rightPos));
            }
        }
        else if ((platformPosition == STATE.RIGHT && state == STATE.LEFT) || (platformPosition == STATE.LEFT && state == STATE.RIGHT))
        {
            platformPosition = STATE.MIDDLE;
            StartCoroutine(MoveToPosition(startPos));
        }
        else isActive = false;

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
        isActive = false;
    }
}
