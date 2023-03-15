using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolModel : MonoBehaviour
{
    [SerializeField] List<GameObject> items;
    [SerializeField] private GameObject prefab;
    
    public virtual void AddToPool(GameObject item)
    {
        items.Add(item);
        item.transform.SetParent(transform);
    }
    
    public virtual T GetDeactiveItem<T>()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].gameObject.activeInHierarchy == false)
            {
                return items[i].GetComponent<T>();
            }
        }

        if (prefab)
        {
            if (prefab.TryGetComponent(out T component))
            {
                GameObject instantiatedObj = Instantiate(prefab);
                AddToPool(instantiatedObj);
                return instantiatedObj.GetComponent<T>();
            }
        }

        return default(T);
    }
    
    public void ResetPool()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }
    }

    private void getItemsFromChildren()
    {
        if (items == null)
            items = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject item = transform.GetChild(i).gameObject;
            if (item != null)
            {
                item.gameObject.SetActive(false);
                items.Add(item);
            }
        }
    }

#if UNITY_EDITOR
    [EditorButton]
    public void InitializeOnEditor()
    {
        Undo.RecordObject(this, "GetItems");
        if (items != null)
            items.Clear();

        getItemsFromChildren();
    }
#endif

    private void Reset()
    {
        transform.ResetLocal();
    }
}
