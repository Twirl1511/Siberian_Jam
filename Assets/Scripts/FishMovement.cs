using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    private Camera _camera;
    private Vector2 mouseWorldPos;
    private Rigidbody2D rigi;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    void Update()
    {
        mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate() {
        MoveToMouse();
        RotateToMouse();
    }

    private void MoveToMouse()
    {
        Vector2 newPosition = transform.position;
        newPosition.x = mouseWorldPos.x;
        rigi.MovePosition(Vector2.Lerp(transform.position, newPosition, Time.deltaTime * _speed));
    }

    private void RotateToMouse()
    {
        mouseWorldPos.y = transform.position.y;
        Quaternion newRotation = Quaternion.LookRotation(((Vector3)mouseWorldPos - transform.position).normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * _rotateSpeed);
    }
}
