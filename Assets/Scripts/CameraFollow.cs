using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    [SerializeField] private float maxY = 1000f;
    [SerializeField] private float minY = -1000f;
    [SerializeField] private float maxX = 1000f;
    [SerializeField] private float minX = -1000f;
    [HideInInspector] public Camera _camera;
    private bool IsActive = false;

    private Vector3 _basePos;

    public Vector3 target_Offset;

    private void Start()
    {
        _camera = Camera.main;
        _basePos = transform.position;
    }
    void Update()
    {
        if (IsActive && _target)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), 0.2f);
            if(newPos.x > maxX) newPos.x = maxX;
            if(newPos.x < minX) newPos.x = minX;
            if(newPos.y > maxY) newPos.y = maxY;
            if(newPos.y < minY) newPos.y = minY;
            transform.position = newPos;
        }
    }

    public void Enable()
    {
        target_Offset = transform.position - _target.position;
        _camera.DOOrthoSize(6.6f, 1f);
        IsActive = true;
    }

    public void Reset()
    {
        IsActive = false;
        _camera.orthographicSize = 8.68f;
        transform.position = _basePos;
    }
}
