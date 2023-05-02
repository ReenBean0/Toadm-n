using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.AnnotationUtility;
using UnityEngine.UIElements;

public class Switch : InteractableResponse
{
    [SerializeField] bool isPermanent;

    private void OnValidate()
    {
        pressDuration = -1;
    }

    public override void Interact()
    {
        base.Interact();
        if (!isActive)
        {
            transform.localPosition = onPosition;
            transform.localRotation = onRotation;
            ActivateRisingEdge();
        }
        else
        {
            if (!isPermanent)
            {
                transform.localPosition = offPosition;
                transform.localRotation = offRotation;
                ActivateFallingEdge();
            }
        }
    }
}
