using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private LayerMask _planeMask;
    [SerializeField] private GameObject _movementPlane;
    [SerializeField] private AnimationCurve _movementCurve;
    private Camera _camera;
    private Vector3 movePosition;
    private Rigidbody2D rigi;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        //Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _planeMask);

        if(hit.collider != null && hit.collider.gameObject == _movementPlane)
        {
            movePosition = hit.point;
        }
    }

    private void FixedUpdate() 
    {
        if (!MenuController.IsPaused)
        {
            MoveToMouse();
            RotateToMouse();
        } 
    }

    private void MoveToMouse()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = movePosition.x;
        float distance = transform.position.x - movePosition.x;
        float eval = _movementCurve.Evaluate(-Mathf.Abs(distance));
        rigi.MovePosition(Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _speed * eval));
    }

    private void RotateToMouse()
    {
        movePosition.y = transform.position.y;
        float distance = Mathf.Abs(transform.position.x - movePosition.x);
        if(distance > 0f)
        {
            Quaternion newRotation = Quaternion.LookRotation(((Vector3)movePosition - transform.position).normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * _rotateSpeed);
        }
    }
}
