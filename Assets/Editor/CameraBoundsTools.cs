using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Adds two buttons to editor to make camera bounds setup less painful
/// - Henry Paul
/// </summary>
[CustomEditor(typeof(CameraBounds), true)]
public class CameraBoundsTools : Editor
{
    SerializedProperty targetScale;
    SerializedProperty targetPosition;
    Camera mainCamera;

    private void OnEnable()
    {
        targetScale = serializedObject.FindProperty("targetScale");
        targetPosition = serializedObject.FindProperty("targetCamPos");
        mainCamera = FindObjectOfType<Camera>();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (GUILayout.Button("Set target"))
        {
            targetScale.floatValue = mainCamera.orthographicSize;
            targetPosition.vector3Value = mainCamera.transform.position;
            serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("Preview target"))
        {
            mainCamera.orthographicSize = targetScale.floatValue;
            mainCamera.transform.position = targetPosition.vector3Value;
            serializedObject.ApplyModifiedProperties();
        }
        base.OnInspectorGUI();
    }
}
