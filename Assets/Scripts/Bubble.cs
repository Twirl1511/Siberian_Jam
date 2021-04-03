using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bubble : MonoBehaviour
{
    [SerializeField] private float _touchLifetimeCoeff = 0.1f;

    [SerializeField] private float _pushForce = 0.2f;

    public float gravityScale = 1.0f;

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
            //Destroy bubble after touch any decoration
            float scale = transform.localScale.x;
            Destroy(gameObject, _touchLifetimeCoeff / scale);

            //Push touched object
            Vector3 direction = (block.Rigi.transform.position - transform.position).normalized;
            direction *= _pushForce;
            direction.x *= 10;
            block.Rigi.AddForce(direction, ForceMode.Force);
        }

        if(collision.gameObject.TryGetComponent(out Bubble bubble))
        {
            Destroy(gameObject);
        }
    }
}