using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IActivatableObject
{
    [SerializeField] protected Vector3 onPosition;
    [SerializeField] protected Quaternion onRotation;
    [SerializeField] protected Vector3 onScale;
    protected Vector3 offPosition;
    protected Quaternion offRotation;
    protected Vector3 offScale;

    public bool isActive = false;

    public abstract void Interact();

    // Start is called before the first frame update
    public virtual void Start()
    {
        offPosition = gameObject.transform.localPosition;
        offRotation = gameObject.transform.localRotation;
        offScale = gameObject.transform.localScale;
    }


}
