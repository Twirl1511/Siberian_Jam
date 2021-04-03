using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsActive = true;
    [HideInInspector] public Rigidbody Rigi;
    [SerializeField] private float _outOfWaterGravity = 3;
    [SerializeField] private float _massChangeDuration = 0f;
    [HideInInspector] public float RayCastLength;
    private RaycastHit _raycastHit;
    [SerializeField] private LayerMask _layerMask;

    private Coroutine _addMassCoroutine;
    private Coroutine _removeMassCoroutine;
    private float _elapsedCoroutineTime = 0f;
    private float _underwaterMass = 0f;
    private float _outwaterMass = 0f;
    private float _currentMass = 0f;

    private void Start() {
        Rigi = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(RayCastToFloor), 1, 1);
        _underwaterMass = Rigi.mass;
        _outwaterMass = Rigi.mass * _outOfWaterGravity;
        _currentMass = _underwaterMass;
    }

    public void OutOfWater()
    {
        if(_removeMassCoroutine != null) StopCoroutine(_removeMassCoroutine);
        if(_addMassCoroutine != null) StopCoroutine(_addMassCoroutine);
        _addMassCoroutine = StartCoroutine(AddMass());
    }

    public void InWater()
    {
        if(_addMassCoroutine != null) StopCoroutine(_addMassCoroutine);
        if(_removeMassCoroutine != null) StopCoroutine(_removeMassCoroutine);
        _removeMassCoroutine = StartCoroutine(RemoveMass());
    }

    private void Update() {
        if(_elapsedCoroutineTime < _massChangeDuration)
        {
            _elapsedCoroutineTime += Time.deltaTime;
        }
    }

    private IEnumerator AddMass()
    {
        _currentMass = Rigi.mass;
        _elapsedCoroutineTime = 0f;
        float shiftCoeff = _currentMass / _underwaterMass;
        while(_elapsedCoroutineTime < _massChangeDuration)
        {
            yield return new WaitForFixedUpdate();
            Rigi.mass = Mathf.Lerp(_currentMass, _outwaterMass, _elapsedCoroutineTime / _massChangeDuration);
        }
    }

    private IEnumerator RemoveMass()
    {
        _currentMass = Rigi.mass;
        _elapsedCoroutineTime = 0f;
        float shiftCoeff = _currentMass / _underwaterMass;
        while(_elapsedCoroutineTime < _massChangeDuration)
        {
            yield return new WaitForFixedUpdate();
            Rigi.mass = Mathf.Lerp(_currentMass, _underwaterMass, _elapsedCoroutineTime / _massChangeDuration);
        }
    }

    public void RayCastToFloor()
    {
        Physics.Raycast(transform.position, Vector3.down, out _raycastHit, Mathf.Infinity, _layerMask);
        if(_raycastHit.distance > RayCastLength)
        {
            RayCastLength = _raycastHit.distance;
        }
        print(RayCastLength);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out SandParticleSystem sand))
        {
            RayCastLength = 0;
        }
    }
}
