using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/* Written by Rian
 * A rework of Henry's code "InteractableDevice"
 * specifically for switches
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
