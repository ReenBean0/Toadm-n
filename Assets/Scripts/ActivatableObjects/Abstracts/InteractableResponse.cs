using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableResponse : Interactable
{
    [SerializeField] protected List<GameObject> risingEdgeTargets;
    [SerializeField] protected List<GameObject> fallingEdgeTargets;

    [SerializeField] protected float pressDuration = 5;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Tongue"))
        {
            Interact();
        }
    }

    protected void ActivateFallingEdge()
    {
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
        if (risingEdgeTargets != null)
        {
            foreach (GameObject gameObject in risingEdgeTargets)
            {
                gameObject.GetComponent<IActivatableObject>().Interact();
            }
        }
        isActive = true;
    }
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
}
