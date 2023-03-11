using System;
using UnityEngine;

public class SerializableScriptableObject : ScriptableObject
{
    public string Guid { get; private set; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        var path = UnityEditor.AssetDatabase.GetAssetPath(this);
        Guid = UnityEditor.AssetDatabase.AssetPathToGUID(path);
    }
#endif
}