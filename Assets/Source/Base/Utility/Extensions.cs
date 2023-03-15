using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public static class Extensions
{
    #region Unity Properties
    public static void SetActiveGameObject(this Component comp, bool statue)
    {
        comp.gameObject.SetActive(statue);
    }

    public static Transform ResetLocal(this Transform target)
    {
        target.localPosition = Vector3.zero;
        target.localRotation = Quaternion.identity;
        target.localScale = Vector3.one;
        return target;
    }

    public static TextMeshProUGUI SetText(this Button btn, string value)
    {
        if (btn.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            btn.GetComponentInChildren<TextMeshProUGUI>().text = value;
            return btn.GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            if (btn.GetComponent<TextMeshProUGUI>() != null)
            {
                btn.GetComponent<TextMeshProUGUI>().text = value;
                return btn.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                return null;
            }
        }
    }

    #endregion

    #region Collections

    public static T GetRandom<T>(this IEnumerable<T> values)
    {
        return values.ElementAt(UnityEngine.Random.Range(0, values.Count()));
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T[] Insert<T>(this T[] array, int index, T obj)
    {
        var list = array.ToList();
        list.Insert(index, obj);
        return list.ToArray();
    }

    #endregion
}
