using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RemoveFromTag))]
[CanEditMultipleObjects]
public class RemoveFromTagEditor : Editor
{
    private SerializedProperty _tagProperty;

    private void OnEnable()
    {
        _tagProperty = serializedObject.FindProperty("tag");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _tagProperty.stringValue = EditorGUILayout.TagField(_tagProperty.stringValue);
        serializedObject.ApplyModifiedProperties();
    }
}