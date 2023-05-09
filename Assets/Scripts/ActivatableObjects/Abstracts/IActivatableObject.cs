using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Added by Henry. A simple interface which declares the intention for
 * an object to do something when interacted with. Can then be added to
 * any interactable object's list of things to activate.
 */
public interface IActivatableObject
{
    public void Interact();
}
