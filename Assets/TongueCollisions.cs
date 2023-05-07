using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCollisions : MonoBehaviour
{
    GameObject toad;

    private void Start()
    {
        toad = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Interactables") && collision.gameObject.tag != "Player" && collision.gameObject.layer != LayerMask.NameToLayer("CameraBounds"))
            {
                Debug.Log($"{collision.gameObject.name} interrupted tongue animation");

                // Tongue has collided with something that isn't interactable, therefore stop tongue animation
                toad.GetComponent<TongueController>().InterruptAnimation();
            }
        }
    }
}
