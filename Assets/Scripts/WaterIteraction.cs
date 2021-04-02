using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIteraction : MonoBehaviour
{
    [SerializeField] private WaterUp Water;
    [SerializeField] private float _outOfWaterGravity;
    private List<Rigidbody> _objectsOutOfWater = new List<Rigidbody>();

    private void Start() 
    {
        InvokeRepeating(nameof(CheckConditions), 2f, 2f);
    }

    private void CheckConditions()
    {
        foreach(Rigidbody r in _objectsOutOfWater)
        {
            Block b = r.GetComponent<Block>();
            if(r.velocity.magnitude <= 0f && b.IsActive)
            {
                print("Victory");
                Water.Up();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Block block))
        {
            Rigidbody rigi = collision.GetComponent<Rigidbody>();

            rigi.mass *= _outOfWaterGravity;
            _objectsOutOfWater.Add(rigi);
        }
        if (collision.TryGetComponent(out Bubble bubble))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Block block))
        {
            Rigidbody rigi = block.GetComponent<Rigidbody>();

            rigi.mass /= _outOfWaterGravity;

            if (_objectsOutOfWater.Contains(rigi))
                _objectsOutOfWater.Remove(rigi);
                
            if(!block.IsActive)
                block.IsActive = true;
        }
    }
}
