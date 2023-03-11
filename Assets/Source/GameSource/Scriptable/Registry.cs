using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Registry : ScriptableObject
{
    [SerializeField] protected List<SerializableScriptableObject> _descriptors = new ();

    public SerializableScriptableObject FindByGuid(string guid)
    {
        return _descriptors.FirstOrDefault(desc => desc.Guid == guid);
    }
}