using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for interactable objects that behave as buttons or switches
/// - Henry Paul
/// </summary>
public class InteractableDevice : MonoBehaviour
{
    [SerializeField] bool isSwitch;

    [SerializeField] GameObject[] risingEdgeTargetObjects; // target objects to be interacted with when toggle turns on
    [SerializeField] GameObject[] fallingEdgeTargetObjects; // target objects to be interacted with when toggle turns off

    // Button unique variable
    [SerializeField] float pressDuration;

    string status;

    // Start is called before the first frame update
    void Start()
    {
        status = "OFF";
        if (isSwitch)
        {
            pressDuration = -1;
        }
    }

    // what do when switch off?!?
    void ActivateFallingEdge()
    {
        foreach (GameObject gameObject in fallingEdgeTargetObjects)
        {
            gameObject.GetComponent<IActivatableObject>().InteractOff();
        }
        status = "OFF";
    }

    // what do when switch on?!! ?
    void ActivateRisingEdge()
    {
        foreach (GameObject gameObject in risingEdgeTargetObjects)
        {
            gameObject.GetComponent<IActivatableObject>().InteractOn();
        }
        status = "ON";
    }

    void Interact()
    {
        if (status == "OFF")
        {
            ActivateRisingEdge();
            if (!isSwitch)
            {
                StartCoroutine(ButtonPress());
            }
        }
        else if (status == "ON")
        {
            // If this object is a switch, the player should be able to manually toggle switch
            // If it is a button, the switch will toggle automatically after the press duration has passed
            if (isSwitch)
            {
                ActivateFallingEdge();
            }
        }
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(pressDuration);

        ActivateFallingEdge();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{name} collision with {collision.gameObject.name}");
        if (collision.gameObject.name.Contains("Tongue"))
        {
            Interact();
        }
    }
}
