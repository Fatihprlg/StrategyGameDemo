using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraController : ControllerBase
{
    
    [Header("Movement Options")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _speedBoost;

    [Header("Zoom Options")]
    [SerializeField] private float _maxZoom;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _zoomSensitivity;
    
    [Header("Ref")] 
    [SerializeField] private Transform _camTarget;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private CinemachineVirtualCamera activeCamera;
    
    public static Camera MainCamera { get; private set; }
    public CinemachineVirtualCamera ActiveCamera => activeCamera;

    private float _maxY;
    private float _minY;
    private float _minX;
    private float _maxX;
    
    private float _aspectRatio;
    private float _halfGridSize = 32;
    
    public override void Initialize()
    {
        MainCamera = Camera.main;
        _aspectRatio = Screen.width / (float)Screen.height;
        ReArrangeOrthoSize();
    }

    public void ChangeCamera(int index)
    {
        activeCamera.SetActiveGameObject(false);
        activeCamera = cameras[index];
        activeCamera.SetActiveGameObject(true);
    }

    public void SetGridHalfSize(float size)
    {
        _halfGridSize = size;
    }

    public override void ControllerLateUpdate(GameStates currentState)
    {
        if(currentState != GameStates.Game) return;
        
        Zoom();
        Move();
    }
    
    private static Vector3 GetDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        return direction.normalized;
    }
    
    private void Zoom()
    {
        if(Input.mouseScrollDelta.magnitude < .1f) return;
        if(EventSystem.current.IsPointerOverGameObject()) return;

        ReArrangeOrthoSize();
    }

    private void ReArrangeOrthoSize()
    {
        float newValue = ActiveCamera.m_Lens.OrthographicSize - Input.mouseScrollDelta.y * _zoomSensitivity;
        float clampedZoom = Mathf.Clamp(newValue, _minZoom, _maxZoom);
        ActiveCamera.m_Lens.OrthographicSize = clampedZoom;
        var gridSizeAsUnits = _halfGridSize * Constants.Numerical.CELL_SCALE_AS_UNIT;
        _maxY = (gridSizeAsUnits * 2) - clampedZoom + .5f;
        _maxX = gridSizeAsUnits + (_maxY - gridSizeAsUnits) / _aspectRatio + .5f;
        _minY = clampedZoom - .5f;
        _minX = gridSizeAsUnits * 2 - _maxX -.5f;
    }

    private void Move()
    {
        var currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _moveSpeed + _speedBoost : _moveSpeed;
        Vector3 direction = GetDirection();
        Vector3 newValue = _camTarget.position + Time.deltaTime * currentSpeed * direction;
        newValue.y = Mathf.Clamp(newValue.y, _minY, _maxY);
        newValue.x = Mathf.Clamp(newValue.x, _minX, _maxX);
        _camTarget.position = newValue;
    }
    
    private void Reset()
    {
        activeCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (ActiveCamera != null)
        {
            if (cameras.Length == 0)
            {
                cameras = new []
                {
                    ActiveCamera
                };
            }
        }
    }
}
