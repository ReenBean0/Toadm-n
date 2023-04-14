using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour, IActivatableObject
{
    Vector3 onPosition;
    Vector3 onScale;

    Vector3 offPosition;
    Vector3 offScale;

    public void Start()
    {
        onPosition = new Vector3(-6.5f, 2.8f, 0.0f);
        onScale = new Vector3(0.5f, 0.170625f, 1);

        offPosition = new Vector3(-6.5f, 2.55f, 0.0f);
        offScale = new Vector3(0.5f, 0.35f, 1);

        InteractOff();
    }

    public void InteractOff()
    {
        transform.localScale = offScale;
        transform.position = offPosition;
    }

    public void InteractOn()
    {
        transform.localScale = onScale;
        transform.position = onPosition;
    }
}
