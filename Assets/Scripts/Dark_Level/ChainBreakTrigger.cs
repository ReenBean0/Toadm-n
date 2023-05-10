using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cave level specific - when the fired projectile hits this trigger, the chain "breaks", 
/// allowing the door to be opened without pulling the platform back in (which would usually stop the player from being able to actually get back up again)
/// - Henry Paul
/// </summary>
public class ChainBreakTrigger : MonoBehaviour
{
    [SerializeField] GameObject leftHandle;
    [SerializeField] GameObject rightHandle;

    [SerializeField] GameObject pullablePlatform;
    [SerializeField] GameObject chainedDoor;
    [SerializeField] GameObject chainPull;
    [SerializeField] GameObject chainBreak;
    [SerializeField] GameObject projectileLauncher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Chain broken");
            leftHandle.GetComponent<InteractableResponse>().RemoveObjectFromRisingEdge(pullablePlatform);
            rightHandle.GetComponent<InteractableResponse>().RemoveObjectFromRisingEdge(chainedDoor);
            rightHandle.GetComponent<InteractableResponse>().RemoveObjectFromRisingEdge(chainPull);
            Destroy(chainBreak);
            projectileLauncher.GetComponent<FireProjectile>().KillProjectile();
        }
    }
}
