using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour, IActivatableObject
{
    [SerializeField] Vector3 onPosition;
    [SerializeField] Vector3 onScale;

    [SerializeField] Vector3 offPosition;
    [SerializeField] Vector3 offScale;

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
        transform.localScale = offScale;
        transform.localPosition = offPosition;
    }

    public void InteractOn()
    {
        isOn = true;
        transform.localScale = onScale;
        transform.localPosition = onPosition;
    }
}
