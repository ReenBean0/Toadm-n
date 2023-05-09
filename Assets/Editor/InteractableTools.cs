using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//Made by Rian
//Adds a UI element to the unity editor to make setting object destinations quicker

[CustomEditor(typeof(Interactable), true)]
class InteractableTools : Editor
{
    Interactable interactable;
    SerializedProperty _position;
    SerializedProperty _rotation;

    private void OnEnable()
    {
        interactable = (Interactable)target;
        _position = serializedObject.FindProperty("onPosition");
        _rotation = serializedObject.FindProperty("onRotation");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (GUILayout.Button("Set Destination"))
        {
            _position.vector3Value = interactable.gameObject.transform.localPosition;
            _rotation.quaternionValue = interactable.gameObject.transform.localRotation;
            serializedObject.ApplyModifiedProperties();
        }
        base.OnInspectorGUI();
    }
}
