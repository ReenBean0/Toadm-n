using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableObjectDetection : MonoBehaviour
{
    [SerializeField] private float radius; 
    [SerializeField] private LayerMask layerMask; // all interactable objects will need to be on this layermask to be detected
                                                  // I've created an "interactables" layermask and set this layermask to that
    private bool setPosisitionCheck = false; //a check to see if flies position has been moved to interable object position


    private void Update()
    {
        //casts a circle with a given radius, at the position of the object this script is attached to, then returns the object(hit)
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero,0f,layerMask);
        //sets the position of flies to the interactable object's postion once, while still in the flies radius
        if (hit)
        {
            if (!setPosisitionCheck)
            {
            transform.position = hit.transform.position;
            }
            setPosisitionCheck = true;
        }
        else
        {
            setPosisitionCheck = false;
        }
    }

}
