using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableResponse
{
    private bool sittingOnButton = false;

    public override void Interact()
    {
        base.Interact();
        if(!isActive)
        {
            transform.localPosition = onPosition;
            transform.localRotation = onRotation;

            ActivateRisingEdge();

            StartCoroutine(ButtonPress());
        }
        else
        {
            transform.localPosition = offPosition;
            transform.localRotation = offRotation;
        }
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(pressDuration);

        if(!sittingOnButton)
        {
            ActivateFallingEdge();
        }
        else
        {
            StartCoroutine(ButtonPress());
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Interactables"))
        {
            if (collision.gameObject.name.Contains("Tongue"))
            {
                Interact();
            }
            if (collision.gameObject.name.Contains("Toad"))
            {
                sittingOnButton = true;
                Interact();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sittingOnButton = false;
    }
}
