using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Written by Rian
 * A rework of Henry's code "InteractableDevice"
 * Specifically for buttons
 */

public class Button : InteractableResponse
{
    private bool sittingOnButton = false; 

    public override void Interact()
    {
        base.Interact();
        if(!isActive)
        {
            //A button can only be pressed, so there's no else statement here
            //If activated, it'll turn on and then wait for the button duration to end.
            ActivateRisingEdge();

            StartCoroutine(ButtonPress());
        }
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(pressDuration);

        //if the Toadm'n is sitting on the button, don't turn back off and start the wait again.
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
        //This checks whether the tongue or toad have collided with the button. If the
        //toad has collided with the button, it'll set a flag that its sitting on the button,
        //and as long as the toad sits on the button it won't turn back off.
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
        //no longer sitting about
        sittingOnButton = false;
    }
}
