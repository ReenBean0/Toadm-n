using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SunTrigger : MonoBehaviour
{
    [SerializeField] GameObject sun;
    [SerializeField] float enterTriggerSunIntensity;
    [SerializeField] float exitTriggerSunIntensity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sun != null)
        {
            sun.GetComponent<Light2D>().intensity = enterTriggerSunIntensity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (sun != null)
        {
            sun.GetComponent<Light2D>().intensity = exitTriggerSunIntensity;
        }
    }
}
