using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// The confusingly named "ComboLockLight" was designed specifically for the cave level
/// There are three of them that are enabled when a red object moves into the "lock" turning the light green
/// When all lights are green, a door opens
/// - Henry Paul
/// </summary>
public class ComboLockLight : ObjectResponse
{
    [SerializeField] Color offColour;
    [SerializeField] Color onColour;
    [SerializeField] float turnOnDelay;
    [SerializeField] GameObject objectToUnlock;
    [SerializeField] GameObject[] otherLocks;

    SpriteRenderer spriteRender;

    public override void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.color = offColour;
        GetComponent<Light2D>().color = offColour;
    }

    public override void Interact()
    {
        base.Interact();
        if (isActive)
        {
            isActive = false;
            TurnOff();
        }
        else
        {
            isActive = true;
            StartCoroutine(WaitAndTurnOn(turnOnDelay));
        }
    }

    public IEnumerator WaitAndTurnOn(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        TurnOn();
    }

    public void TurnOn()
    {
        spriteRender.color = onColour;
        GetComponent<Light2D>().color = onColour;

        bool allLocks = true;
        // If other locks are active or 'unlocked' then trigger target object
        foreach (GameObject otherLock in otherLocks)
        {
            if (!otherLock.GetComponent<ComboLockLight>().isActive)
            {
                allLocks = false;
            }
        }

        if (allLocks)
        {
            objectToUnlock.GetComponent<IActivatableObject>().Interact();
        }
    }

    public void TurnOff()
    {
        spriteRender.color = offColour;
    }
}
