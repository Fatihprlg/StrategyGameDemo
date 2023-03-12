using UnityEngine;

[RequireComponent(typeof(MapEntity))]
public class MapEntityBehaviour : MonoBehaviour
{
    protected MapEntity _attachedEntity;
    
    public virtual void Init(MapEntity attachedEntity)
    {
        _attachedEntity = attachedEntity;
    }
}