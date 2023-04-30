using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IActivatableObject
{
    [SerializeField] public Vector3 onPosition;
    [SerializeField] public Quaternion onRotation;
    protected Vector3 offPosition;
    protected Quaternion offRotation;

    public bool isActive = false;

    public abstract void Interact();

    // Start is called before the first frame update
    public virtual void Start()
    {
        offPosition = gameObject.transform.localPosition;
        offRotation = gameObject.transform.localRotation;
    }



}