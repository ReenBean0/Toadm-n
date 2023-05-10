using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* Written by Rian
 * A rework of Henry's code "InteractableDevice"
 * Specifically for handles, used on pullable/draggable objects
 */
public class Handle : InteractableResponse
{
    public enum Direction { LEFT, RIGHT }
    [SerializeField] public Direction direction;

    public override void Interact()
    {
        base.Interact();
        if (!isActive)
        {
            SetPos();

            PullObjects();

            StartCoroutine(ButtonPress());
        }
        else
        {
            SetPos();
        }
    }

    void PullObjects()
    {
        foreach (GameObject gameObject in risingEdgeTargets)
        {
            PullableObject obj = gameObject.GetComponent<PullableObject>();
            if (!obj.isActive)
            {
                if (direction == Direction.LEFT && obj.PlatformPosition > 0) obj.PlatformPosition -= 1;
                else if (direction == Direction.RIGHT && obj.PlatformPosition < obj.anchors.Count - 1) obj.PlatformPosition += 1;

                gameObject.GetComponent<IActivatableObject>().Interact();
            }
        }
        isActive = true;
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(pressDuration);
        SetPos();
        isActive = false;
    }
}
