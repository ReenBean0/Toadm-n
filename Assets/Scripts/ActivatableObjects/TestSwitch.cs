using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitch : MonoBehaviour, IActivatableObject
{
    Vector3 onPosition;
    Vector3 onRotation;

    Vector3 offPosition;
    Vector3 offRotation;

    public void Start()
    {
        onPosition = new Vector3(2.5f, 3.5f, 0.0f);
        onRotation = new Vector3(0, 0, -45);

        offPosition = new Vector3(2.5f, 3.1f, 0.0f);
        offRotation = new Vector3(0, 0, 35);

        InteractOff();
    }

    public void InteractOff()
    {
        transform.position = offPosition;
        transform.rotation = Quaternion.Euler(offRotation);
    }

    public void InteractOn()
    {
        transform.position = onPosition;
        transform.rotation = Quaternion.Euler(onRotation);
    }
}
