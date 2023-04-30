using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectNew : ObjectResponse
{
    public override void Interact()
    {
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
