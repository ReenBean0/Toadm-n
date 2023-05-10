using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Written by Rian
 * Reworking what Henry had done on interactable devices.
 * Here's some trivia, before using this abstract script to create interactables such as switches, buttons, etc,
 * every interactable had two seperate scripts which referenced eachother, as well as a very annoying way of placing
 * them where you manually enter their starding and ending positions, rotations, scale for every single object in a scene...
 * Well, no more! Now you can design interactables to be much simpler, only requiring one script (usually a child of this one)
 * Object response is handled in ObjectResponse
 */
public abstract class InteractableResponse : Interactable
{
    [SerializeField] protected List<GameObject> risingEdgeTargets; //when turned off
    [SerializeField] protected List<GameObject> fallingEdgeTargets; //when turned on

    [SerializeField] protected float pressDuration = 5; //how long until it resets on its own

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if hit by tongue, activates Interact feature, which is overrided in the child classes
        if (collision.gameObject.name.Contains("Tongue"))
        {
            Interact();
        }
    }

    protected void ActivateFallingEdge()
    {
        //When turned on, its position is moved to appear on and each assigned object will react to the state change.
        SetPos();
        if (fallingEdgeTargets != null)
        {
            foreach (GameObject gameObject in fallingEdgeTargets)
            {
                gameObject.GetComponent<IActivatableObject>().Interact();
            }
        }
        isActive = false;
    }

    protected void ActivateRisingEdge()
    {
        //same as before, but for being turned off
        SetPos();
        if (risingEdgeTargets != null)
        {
            foreach (GameObject gameObject in risingEdgeTargets)
            {
                gameObject.GetComponent<IActivatableObject>().Interact();
            }
        }
        isActive = true;
    }

    #region Added by Henry
    public void RemoveObjectFromRisingEdge(GameObject objectToRemove)
    {
        if (risingEdgeTargets != null)
        {
            risingEdgeTargets.Remove(objectToRemove);
        }
    }

    public void AddObjectToRisingEdge(GameObject objectToAdd)
    {
        if (risingEdgeTargets != null)
        {
            risingEdgeTargets.Add(objectToAdd);
        }
    }

    public void RemoveObjectFromFallingEdge(GameObject objectToRemove)
    {
        if (fallingEdgeTargets != null)
        {
            fallingEdgeTargets.Remove(objectToRemove);
        }
    }

    public void AddObjectToFallingEdge(GameObject objectToAdd)
    {
        if (fallingEdgeTargets != null)
        {
            fallingEdgeTargets.Add(objectToAdd);
        }
    }
    #endregion
    protected void SetPos()
    {
        //I noticed that every interactable had this code in their Interact override, so I made this method and
        //call it in the rising and falling edge methods. The object's state is tied to whether the object if active or not.
        if (isActive)
        {
            transform.localPosition = offPosition;
            transform.localRotation = offRotation;
            transform.localScale = offScale;
        }
        else
        {
            transform.localPosition = onPosition;
            transform.localRotation = onRotation;
            transform.localScale = onScale;
        }
    }
}
