using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Registry : ScriptableObject
{
    [SerializeField] protected List<SerializableScriptableObject> _descriptors = new ();

    public SerializableScriptableObject FindByGuid(string guid)
    {
        foreach (var item in _descriptors)
        {
            if (item.Guid == guid) return item;
        }
        return default;
    }
}