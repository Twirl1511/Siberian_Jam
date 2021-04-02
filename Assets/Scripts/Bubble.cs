using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bubble : MonoBehaviour
{
    [SerializeField] private float _bubbleExplodeTime;

    [SerializeField] private float _pushForce = 0.2f;
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object

    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    Rigidbody m_rb;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Block block))
        {
            Destroy(gameObject, _bubbleExplodeTime);
        }
        if(collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            Vector3 direction = (rb.transform.position - transform.position).normalized;
            direction *= _pushForce;
            direction.x *= 10;
            rb.AddForce(direction, ForceMode.Force);
        }
        
    }
}
