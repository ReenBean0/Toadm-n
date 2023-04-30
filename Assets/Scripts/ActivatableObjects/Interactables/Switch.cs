using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.AnnotationUtility;
using UnityEngine.UIElements;

public class Switch : InteractableResponse
{
    private void OnValidate()
    {
        pressDuration = -1;
    }

    public override void Interact()
    {
        if (!isActive)
        {
            transform.localScale = onScale;
            transform.localPosition = onPosition;
            transform.localRotation = onRotation;
            ActivateRisingEdge();
        }
        else
        {
            transform.localScale = offScale;
            transform.localPosition = offPosition;
            transform.localRotation = offRotation;
            ActivateFallingEdge();
        }
    }
}
