using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anchors can be set for anchoredobjects to move between states
/// - Rian
/// </summary>
public class MovementAnchor : MonoBehaviour, IActivatableObject
{
    [SerializeField] AnchoredObject.STATE state;
    [SerializeField] AnchoredObject platform;

    private void OnValidate()
    {
        //platform = gameObject.GetComponentInParent<AnchoredObject>();
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
        //transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
