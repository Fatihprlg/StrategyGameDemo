using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityUIView : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI nameTxt;
    [SerializeField] protected TextMeshProUGUI sizeTxt;
    [SerializeField] protected TextMeshProUGUI healthTxt;
    public virtual void SetData(MapEntityData data)
    {
        icon.sprite = data.visual;
        nameTxt.text = data.name;
        sizeTxt.text = $"{data.width}x{data.height}";
        healthTxt.text = $"HP: {data.health.ToString()}";
    }

}