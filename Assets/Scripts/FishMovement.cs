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
        else
        {
            
            MoveWhilePause();

        }
    }


    // механика плавания пока идут катсцены
    private bool _flagIdleMove = true;
    private float _timeForIDLE;
    /// <summary>
    /// IDLE во время паузы
    /// </summary>
    /// <param name="leftPoint"></param>
    /// <param name="rightPoint"></param>
    private void MoveWhilePause()
    {
        Vector3 leftPoint = new Vector3(-3, transform.position.y, transform.position.z);
        Vector3 rightPoint = new Vector3(3, transform.position.y, transform.position.z);
        Vector3 endPosition;
        if (_flagIdleMove)
        {
            endPosition = leftPoint;
        }
        else
        {
            endPosition = rightPoint;
        }
        float distance = transform.position.x - endPosition.x;
        float eval = _movementCurve.Evaluate(-Mathf.Abs(distance));
        rigi.MovePosition(Vector3.Lerp(transform.position, endPosition, Time.deltaTime * _speed * eval));



        Quaternion newRotation = Quaternion.LookRotation(((Vector3)endPosition - transform.position).normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * _rotateSpeed);
        _timeForIDLE += Time.deltaTime;
        if (_timeForIDLE >= 2f)
        {
            _timeForIDLE = 0;
            _flagIdleMove = !_flagIdleMove;
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
