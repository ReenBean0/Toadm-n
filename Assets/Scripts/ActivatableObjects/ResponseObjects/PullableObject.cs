using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Written by Rian
 * A rework of Henry's code "Movable Object"
 * Refers to a pullable/draggable object. In the report this would be
 * the object which travels along a path of nodes
 */


public class PullableObject : ObjectResponse
{

    [SerializeField] public List<GameObject> anchors = new List<GameObject>();
    public int PlatformPosition = 1;

    public override void Start()
    {
        transform.localPosition = anchors[PlatformPosition].gameObject.transform.localPosition;
    }

    public override void Interact()
    {
        base.Interact();
        if (!isActive)
        {
            isActive = true;
            Debug.Log("moving to " + anchors[PlatformPosition].transform.position);
            StartCoroutine(MoveToPosition(anchors[PlatformPosition].transform.localPosition, transform.localRotation, true));
        }
    }
}
