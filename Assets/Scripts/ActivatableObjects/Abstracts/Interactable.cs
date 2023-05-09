using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/* Written by Rian
 * A reworking of the code found in Henry's interactable devices
 * This rework aims to simplify that code, using abstraction to 
 * make it more readable and easier to build levels with.
 */
public abstract class Interactable : MonoBehaviour, IActivatableObject
{
    [SerializeField] public Vector3 onPosition;
    [SerializeField] public Quaternion onRotation;
    protected Vector3 offPosition;
    protected Quaternion offRotation;

    public bool isActive = false;


#region Added by Henry
    [SerializeField] protected bool triggerCameraEvent;
    [SerializeField] protected Vector3 camTargetPos;
    [SerializeField] protected float camTargetScale;
    [SerializeField] protected float cameraPauseBeforeReturn;
    [SerializeField] protected float cameraEventStartDelay;
    protected Vector3 previousCamPos;
    protected float previousCamScale;
#endregion

    public virtual void Interact()
    {
        if (triggerCameraEvent)
        {
            CameraEvent();
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        offPosition = gameObject.transform.localPosition;
        offRotation = gameObject.transform.localRotation;
    }

    void CameraEvent()
    {
        CameraController camController = GameManager.instance.gameObject.GetComponent<CameraController>();
        StartCoroutine(camController.AnimateCameraEvent(camTargetPos, camTargetScale, cameraPauseBeforeReturn, cameraEventStartDelay));
    }
}