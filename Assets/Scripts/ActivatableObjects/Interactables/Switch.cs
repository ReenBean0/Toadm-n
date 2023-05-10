using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/* Written by Rian
 * A rework of Henry's code "InteractableDevice"
 * specifically for switches
 * 
 * switches have a press duration of -1 because they must be toggled on and off sepereately, so they will never activate risingedge on their own.
 */
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
            ActivateRisingEdge();
        }
        else
        {
            if (!isPermanent)
            {
                ActivateFallingEdge();
            }
        }
    }
}
