using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Handle : InteractableResponse
{
    public enum Direction { LEFT, RIGHT }
    [SerializeField] public Direction direction;

    private void OnValidate()
    {
        pressDuration = 0;
    }

    public override void Interact()
    {
        if (!isActive)
        {
            transform.localPosition = onPosition;
            transform.localRotation = onRotation;

            PullObjects();

            StartCoroutine(ButtonPress());
        }
        else
        {
            transform.localPosition = offPosition;
            transform.localRotation = offRotation;
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

        isActive = false;
    }
}
