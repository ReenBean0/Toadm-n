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
    [SerializeField] protected Vector3 onPosition;
    [SerializeField] protected Quaternion onRotation;
    [SerializeField] protected Vector3 onScale;
    protected Vector3 offPosition;
    protected Quaternion offRotation;
    protected Vector3 offScale;

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

    private void OnValidate()
    {
        //This exists because it was added later, and I didn't want to
        //immediately break all existing interactables, so if the new
        //onScale variable was empty for them, it should set them to 
        //whatever their scale currently is (assume that scale doesn't change)
        if(onScale == Vector3.zero)
        {
            onScale = transform.localScale;
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //assumes that the location of the object is where you want it to start in the level
        offPosition = gameObject.transform.localPosition;
        offRotation = gameObject.transform.localRotation;
        offScale = gameObject.transform.localScale;
    }

    void CameraEvent()
    {
        CameraController camController = GameManager.instance.gameObject.GetComponent<CameraController>();
        StartCoroutine(camController.AnimateCameraEvent(camTargetPos, camTargetScale, cameraPauseBeforeReturn, cameraEventStartDelay));
    }
}