using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class AttachToMovingObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if ((obj.name.ToLower().Contains("moving") && obj.name.ToLower().Contains("platform")) || 
            (obj.transform.parent.name.ToLower().Contains("moving") && obj.transform.parent.name.ToLower().Contains("platform")))
        {
            transform.SetParent(obj.transform);
            Debug.Log($"Attached to: {obj.name}");
        }
        else
        {
            transform.SetParent(null);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if ((obj.name.ToLower().Contains("moving") && obj.name.ToLower().Contains("platform")) ||
            (obj.transform.parent.name.ToLower().Contains("moving") && obj.transform.parent.name.ToLower().Contains("platform")))
        {
            Debug.Log("Toad detached");
            transform.SetParent(null);
        }
    }
}
