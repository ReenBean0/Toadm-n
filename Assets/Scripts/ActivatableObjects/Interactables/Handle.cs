using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* Written by Rian
 * A rework of Henry's code "InteractableDevice"
 * Specifically for handles, used on pullable/draggable objects
 * 
 * This one works a little differently from other interactables, but makes use of the inheritence design to produce something unique
 */

public class Handle : InteractableResponse
{
    public enum Direction { LEFT, RIGHT } 
    [SerializeField] public Direction direction;
    //A handle will have a specific direction assigned to it

    public override void Interact()
    {
        //when activated, it will try to find pullable objects in the list and interact with them.
        //From there, it works like a button in that it waits until it can be next used.
        base.Interact();
        if (!isActive)
        {
            SetPos();

            PullObjects();

            StartCoroutine(ButtonPress());
        }
        else
        {
            SetPos();
        }
    }

    void PullObjects()
    {
        foreach (GameObject gameObject in risingEdgeTargets)
        {
            PullableObject obj = gameObject.GetComponent<PullableObject>();
            if (!obj.isActive)
            {
                //if the object is available to be turned on, and isn't at either extreme of its journey, it'll be set to the next position then interact will tell it to move.
                if (direction == Direction.LEFT && obj.PlatformPosition > 0) obj.PlatformPosition -= 1;
                else if (direction == Direction.RIGHT && obj.PlatformPosition < obj.anchors.Count - 1) obj.PlatformPosition += 1;

                gameObject.GetComponent<IActivatableObject>().Interact();
            }
        }
        isActive = true;
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(pressDuration);
        SetPos();
        isActive = false;
    }
}
