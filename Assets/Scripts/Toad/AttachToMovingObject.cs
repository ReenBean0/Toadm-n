using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parents the toads transform to any object that contains "moving" and "platform" in it's name or it's parents name if it has one
/// - Henry Paul
/// </summary>
public class AttachToMovingObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name.ToLower().Contains("moving") && obj.name.ToLower().Contains("platform"))
        {
            transform.SetParent(obj.transform);
            Debug.Log($"Attached to: {obj.name}");
        }
        else
        {
            if (obj.transform.parent != null)
            {
                if (obj.transform.parent.name.ToLower().Contains("moving") && obj.transform.parent.name.ToLower().Contains("platform"))
                {
                    transform.SetParent(obj.transform);
                    Debug.Log($"Attached to: {obj.name}");
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name.ToLower().Contains("moving") && obj.name.ToLower().Contains("platform"))
        {
            Debug.Log("Toad detached");
            transform.SetParent(null);
        }
        else
        {
            if (obj.transform.parent != null)
            {
                if (obj.transform.parent.name.ToLower().Contains("moving") && obj.transform.parent.name.ToLower().Contains("platform"))
                {
                    Debug.Log("Toad detached");
                    transform.SetParent(null);
                }
            }
        }
    }
}
