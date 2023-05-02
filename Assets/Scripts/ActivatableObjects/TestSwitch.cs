using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitch : MonoBehaviour, IActivatableObject
{
    [SerializeField] Vector3 onPosition;
    [SerializeField] Vector3 onRotation;

    [SerializeField] Vector3 offPosition;
    [SerializeField] Vector3 offRotation;

    bool isOn;

    public void Start()
    {
        isOn = false;
        InteractOff();
    }

    public void Interact()
    {
        if (isOn)
        {
            InteractOff();
        }
        else
        {
            InteractOn();
        }
    }

    public void InteractOff()
    {
        isOn = false;
        transform.localPosition = offPosition;
        transform.localRotation = Quaternion.Euler(offRotation);
    }

    public void InteractOn()
    {
        isOn = true;
        transform.localPosition = onPosition;
        transform.localRotation = Quaternion.Euler(onRotation);
    }
}
