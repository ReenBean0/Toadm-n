using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Written by Rian
 * A rework of Henry's code "Movable Object" but using the abstract ObjectResponse class
 * 
 * Henry then added the "InteractWithPause" feature
 */

public class MovingObject : ObjectResponse
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
            StartCoroutine(MoveToPosition(offPosition, offRotation, offScale));
        }
        else
        {
            isActive = true;
            StartCoroutine(MoveToPosition(onPosition, onRotation, onScale));
        }
    }
}
