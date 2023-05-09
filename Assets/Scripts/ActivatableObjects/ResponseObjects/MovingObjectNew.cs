using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Written by Rian
 * A rework of Henry's code "Movable Object"
 */

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
