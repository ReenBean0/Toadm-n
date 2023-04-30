using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!isActive)
        {
            isActive = true;
            Debug.Log("moving to " + anchors[PlatformPosition].transform.position);
            StartCoroutine(MoveToPosition(anchors[PlatformPosition].transform.localPosition, transform.localRotation, true));
        }
    }
}
