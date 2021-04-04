using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bubble : MonoBehaviour
{
    [SerializeField] private float _touchLifetimeCoeff = 0.1f;

    public float _pushForce = 0.2f;
    [SerializeField] private float _pushForceXMulpiplayer = 50f;

    [HideInInspector] public float ActivateLevelY;
    public float gravityScale = 1.0f;

    public static float globalGravity = -9.81f;

    public System.Action<Bubble> OnDestroyEvent;

    Rigidbody m_rb;
    Collider _collider;

    public static int counter = 0;
    public int CurrentCounter;


    void OnEnable()
    {
        counter++;
        CurrentCounter = counter;
        _collider = GetComponent<Collider>();
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
        //_collider.enabled = false;
    }

    private void Start() {

        //Baaaadly!!!!!!!!
        Block[] allBlocks = FindObjectsOfType<Block>();
        foreach(Block block in allBlocks)
        {
            if(block.transform.position.y < ActivateLevelY)
            {
                Collider[] colliders = block.GetComponents<Collider>();
                foreach(var collider in colliders)
                {
                    Physics.IgnoreCollision(_collider, collider, true);
                }
            }
        }
    }

    private void OnDestroy() {
        OnDestroyEvent?.Invoke(this);
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
        //if(!_collider.enabled && transform.position.y >= ActivateLevelY + 2f)
        //{
        //    _collider.enabled = true;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Block block))
        {
            float scale = transform.localScale.x;
            Destroy(gameObject, _touchLifetimeCoeff / scale);

            Vector3 direction = (block.Rigi.transform.position - transform.position).normalized;
            direction *= _pushForce;
            direction.x *= _pushForceXMulpiplayer;
            block.Rigi.AddForce(direction, ForceMode.Force);
        }

        if(collision.gameObject.TryGetComponent(out Bubble bubble))
        {
            if(CurrentCounter > bubble.CurrentCounter)
            {
                Destroy(bubble.gameObject);
            }
            
        }
    }
}