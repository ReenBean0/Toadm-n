using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableObjectDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float interactDelay;
    [SerializeField] private LayerMask layerMask; // all interactable objects will need to be on this layermask to be detected
                                                  // I've created an "interactables" layermask and set this layermask to that
    private bool setPosisitionCheck = false; //a check to see if flies position has been moved to interable object position
    private float currentTime = 0;
    private float nextTime = 0;

    private void Update()
    {
        //casts a circle with a given radius, at the position of the object this script is attached to, then returns the collder2D within this 

        Collider2D overLapCircle = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        //sets the position of flies to the interactable object's postion once, while still in the flies radius

        if (overLapCircle)
        {
            if (!setPosisitionCheck)
            {
                currentTime = Time.time;
                transform.position = overLapCircle.transform.position;
            }
            setPosisitionCheck = true;
            if(currentTime + interactDelay > nextTime)
            {
                nextTime = Time.time;
                this.GetComponent<FlyController>().enabled = false;
            }
            else
            {
                this.GetComponent <FlyController>().enabled = true;
            }
        }
        else
        {
            setPosisitionCheck = false;
        }
    }

}
