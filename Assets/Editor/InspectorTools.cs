using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactable), true)]
class InspectorTools : Editor
{

    Interactable interactable;
    GameObject interactableObject;

    private void OnEnable()
    {
        interactable = (Interactable)target;
        interactableObject = interactable.gameObject;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Set Destination"))
        {
            interactable.onPosition = interactableObject.transform.localPosition;
            interactable.onRotation = interactableObject.transform.localRotation;
        }
        base.OnInspectorGUI();
    }
}