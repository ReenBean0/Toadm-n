using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableResponse : Interactable
{
    [SerializeField] protected GameObject[] risingEdgeTargets;
    [SerializeField] protected GameObject[] fallingEdgeTargets;

    [SerializeField] protected float pressDuration = 5;

    [SerializeField] bool triggerCameraEvent;
    [SerializeField] Vector3 camTargetPos;
    [SerializeField] float camTargetScale;
    [SerializeField] float cameraPauseBeforeReturn;
    [SerializeField] float cameraEventStartDelay;
    protected Vector3 previousCamPos;
    protected float previousCamScale;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Tongue"))
        {
            Interact();

            if (triggerCameraEvent)
            {
                CameraEvent();
            }
        }
    }

    protected void ActivateFallingEdge()
    {
        foreach (GameObject gameObject in fallingEdgeTargets)
        {
            gameObject.GetComponent<IActivatableObject>().Interact();
        }
        isActive = false;
    }

    protected void ActivateRisingEdge()
    {
        foreach (GameObject gameObject in risingEdgeTargets)
        {
            gameObject.GetComponent<IActivatableObject>().Interact();
        }
        isActive = true;
    }

    void CameraEvent()
    {
        CameraController camController = GameManager.instance.gameObject.GetComponent<CameraController>();
        StartCoroutine(camController.AnimateCameraEvent(camTargetPos, camTargetScale, cameraPauseBeforeReturn, cameraEventStartDelay));
    }
}
