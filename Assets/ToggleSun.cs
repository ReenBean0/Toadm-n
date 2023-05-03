using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleSun : ObjectResponse
{
    [SerializeField] float intensitySemiBright;
    [SerializeField] float intensityDark;
    Light2D sunLight;

    private void Start()
    {
        sunLight = GetComponent<Light2D>();
    }

    public override void Interact()
    {
        base.Interact();
        if (sunLight.intensity == intensitySemiBright)
        {
            sunLight.intensity = intensityDark;
        }
        else
        {
            sunLight.intensity = intensitySemiBright;
        }
    }
}
