using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivate : MonoBehaviour, IActivatableObject
{
    [SerializeField] bool isOpen;
    public void Interact()
    {
        isOpen = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen)
        {
            // level complete
            Debug.Log("Level complete");
        }
    }
}
