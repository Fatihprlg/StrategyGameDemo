using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstructor : MonoBehaviour, IInitializable
{
    [SerializeField] private SpriteRenderer ghost;
    private bool constructionState = true;
    
    public void Initialize()
    {
        EventManager.OnBuildingUISelected.AddListener(OnBuildingUISelected);
    }

    private void OnBuildingUISelected(BuildingData data)
    {
        
    }
    

    
}
