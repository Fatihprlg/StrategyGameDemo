using UnityEngine;

/// <summary>
/// Initial positioning elements of infinite scroll view.
/// <see cref="ArrangeChildren"/>
/// <seealso cref="InfiniteScrollView"/>
/// </summary>
public class InfiniteContent : MonoBehaviour
{
    /// <summary>
    /// Arranges the positions.
    /// First child position is above hidden position for making algorithm more readable and understandable.
    /// Arranges the rest downwards to the end.
    /// </summary>
    public void ArrangeChildren(float elementHeight, float spacing)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform[] rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }
        
        float topY = rectTransform.rect.height * .5f;
        float posOffset = elementHeight * .5f + spacing;
        
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.x = 0;
            childPos.y = topY - posOffset - (i - 1) * (elementHeight + spacing);// first one for upper hidden position
            rtChildren[i].localPosition = childPos;
            Vector2 anchoredPos = rtChildren[i].anchoredPosition;
            anchoredPos.x = 0;
            rtChildren[i].anchoredPosition = anchoredPos;
            
            
        }
    }
}
