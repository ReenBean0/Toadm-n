using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Interactable), true)]
class InspectorTools : Editor
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