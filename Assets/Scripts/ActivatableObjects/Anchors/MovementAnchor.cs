using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnchor : MonoBehaviour, IActivatableObject
{
    [SerializeField] AnchoredObject.STATE state;
    AnchoredObject platform;

    private void OnValidate()
    {
        platform = gameObject.GetComponentInParent<AnchoredObject>();
    }

    public void Interact()
    {
        if(platform.isActive)
        {
            //nothing happens until platform stops moving
        }
        else
        {
            platform.InteractOn(state);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
