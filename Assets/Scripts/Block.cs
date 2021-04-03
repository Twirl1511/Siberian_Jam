using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsActive = true;
    [HideInInspector] public Rigidbody Rigi;
    [HideInInspector] public float RayCastLength;
    private RaycastHit _raycastHit;
    [SerializeField] private LayerMask _layerMask;

    private void Start() {
        Rigi = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(RayCastToFloor), 1, 1);
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
