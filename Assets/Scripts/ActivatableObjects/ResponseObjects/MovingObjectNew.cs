using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectNew : ObjectResponse
{
    [SerializeField] float pauseBeforeMove;

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(InteractWithPause());
    }

    IEnumerator InteractWithPause()
    {
        yield return new WaitForSecondsRealtime(pauseBeforeMove);
        if (isActive)
        {
            isActive = false;
            StartCoroutine(MoveToPosition(offPosition, offRotation));
        }
        else
        {
            isActive = true;
            StartCoroutine(MoveToPosition(onPosition, onRotation));
        }
    }
}
