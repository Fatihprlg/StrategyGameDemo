using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles infinite scroll view algorithm.
/// </summary>
public class InfiniteScrollView : MonoBehaviour , IScrollHandler,IDragHandler,IBeginDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private InfiniteContent _infiniteContent;
    [SerializeField] private float elementHeight = 300;
    [SerializeField] private float spacing = 25;
    
    [SerializeField] private RectTransform _rectTransform;
    private bool isUpwards;
    private float upperBound;
    private float lowerBound;
    private Vector2 mousePos;
    
    /// <summary>
    /// Determine <param name="upperBound"> upper bound for sending upmost element to the bottom</param>
    /// <param name="lowerBound"> lower bound for sending downmost element to the top </param>
    /// </summary>
    public void Init()
    {
        _infiniteContent.ArrangeChildren(elementHeight, spacing);
        upperBound = _scrollRect.content.GetChild(0).position.y + spacing;
        lowerBound = _scrollRect.content.GetChild(_scrollRect.content.childCount - 1).position.y - spacing;
    }

    public int GetNecessaryElementCount()
    {
        return Mathf.CeilToInt(_rectTransform.rect.height / elementHeight);
    }
    
    public void OnScroll(PointerEventData eventData)
    {
        isUpwards = eventData.scrollDelta.y < 0;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        isUpwards = eventData.position.y > mousePos.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = eventData.position;
    }

    public void OnScrollValueChanged(Vector2 value)
    {
        if (isUpwards)
        {
            HandleUpwards();
        }
        else
        {
            HandleDownwards();
        }
    }

    /// <summary>
    /// IF scroll going downwards. check for downmost element is passes the lower bound.
    /// if it is send it up to the upmost element's above and set it upmost element.
    /// </summary>
    private void HandleDownwards()
    {
        var downmostElement = _scrollRect.content.GetChild(_scrollRect.content.childCount - 1);
        if(downmostElement.position.y > lowerBound) return;

        var upmostElement = _scrollRect.content.GetChild(0);
        var upPos = upmostElement.localPosition;
        upPos.y = upmostElement.localPosition.y + elementHeight + spacing;
        downmostElement.localPosition = upPos;
        downmostElement.SetSiblingIndex(0);
    }
    /// <summary>
    /// IF scroll going upwards. check for upmost element is passes the upper bound.
    /// if it is send it down to the downmost element's below and set it downmost element.
    /// </summary>
    private void HandleUpwards()
    {
        var upmostElement = _scrollRect.content.GetChild(0);
        if(upmostElement.position.y < upperBound) return;

        var downmostElement = _scrollRect.content.GetChild(_scrollRect.content.childCount - 1);
        var downPos = downmostElement.localPosition;
        downPos.y = downmostElement.localPosition.y - elementHeight - spacing;
        upmostElement.localPosition = downPos;
        upmostElement.SetSiblingIndex(_scrollRect.content.childCount - 1);
    }

}