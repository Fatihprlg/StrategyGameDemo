using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class InputManager
{
    public Vector2 PointerDownPosition;
    public Vector2 PointerPosition;
    public Vector2 PointerUpPosition;
    public Vector2 PointerIdlePosition;
    public Vector2 RightPointerDownPosition;
    public Vector2 RightPointerUpPosition;
    
    public Vector2 DeltaPosition =>
        new ((PointerPosition.x - PointerDownPosition.x) / Screen.width,
            (PointerPosition.y - PointerDownPosition.y) / Screen.height);

    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerEvent;
    public UnityEvent OnPointerUpEvent;
    public UnityEvent OnRightPointerDownEvent;
    public UnityEvent OnRightPointerUpEvent;
    public UnityEvent OnPointerIdleEvent;
    private int UILayer => LayerMask.NameToLayer("UI");
    
    
    public void PointerUpdate()
    {
        OnPointerIdle();
        
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnPointer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            OnRightPointerUp();
        }
    }

    public void OnPointerDown()
    {
        PointerDownPosition = Input.mousePosition;
        OnPointerDownEvent?.Invoke();
    }

    public void OnPointer()
    {
        PointerPosition = Input.mousePosition;
        OnPointerEvent?.Invoke();
    }

    public void OnPointerUp()
    {
        PointerUpPosition = Input.mousePosition;
        OnPointerUpEvent?.Invoke();
    }
    public void OnRightPointerDown()
    {
        RightPointerDownPosition = Input.mousePosition;
        OnRightPointerDownEvent?.Invoke();
    }
    public void OnRightPointerUp()
    {
        RightPointerUpPosition = Input.mousePosition;
        OnRightPointerUpEvent?.Invoke();
    }

    public void OnPointerIdle()
    {
        PointerIdlePosition = Input.mousePosition;
        OnPointerIdleEvent?.Invoke();
    }
    
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
 
 
    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(IEnumerable<RaycastResult> eventSystemRaycastResults)
    {
        return eventSystemRaycastResults.Any(curRaycastResult => curRaycastResult.gameObject.layer == UILayer);
    }
 
 
    //Gets all event system raycast results of current mouse or touch position.
    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new (EventSystem.current)
        {
            position = Input.mousePosition
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}