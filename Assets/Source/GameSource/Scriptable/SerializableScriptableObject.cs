using System;
using UnityEngine;

public class SerializableScriptableObject : ScriptableObject
{
    public string Guid => _guid;
    [SerializeField, HideInInspector]private string _guid;

#if UNITY_EDITOR
    private void OnValidate()
    {
        var path = UnityEditor.AssetDatabase.GetAssetPath(this);
        _guid = UnityEditor.AssetDatabase.AssetPathToGUID(path);
    }
#endif
}