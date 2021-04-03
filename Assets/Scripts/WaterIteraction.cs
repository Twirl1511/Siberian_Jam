using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIteraction : MonoBehaviour
{
    [SerializeField] private WaterUp Water;
    [SerializeField] private float _velocityExt;
    //[SerializeField] private float _outOfWaterGravity;
    public static List<Rigidbody> _objectsOutOfWater = new List<Rigidbody>();

    private void Start() 
    {
        InvokeRepeating(nameof(CheckConditions), 2f, 2f);
    }

    private void CheckConditions()
    {
        foreach(Rigidbody r in _objectsOutOfWater)
        {
            Block b = r.GetComponent<Block>();
            if(r.velocity.magnitude <= _velocityExt && b.IsActive && BubbleControllerScript.Bubbles.Count == 0)
            {
                Water.Up();
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Block block))
        {
            block.OutOfWater();
            //block.Rigi.mass *= _outOfWaterGravity;
            _objectsOutOfWater.Add(block.Rigi);
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
            block.InWater();
            //block.Rigi.mass /= _outOfWaterGravity;

            if (_objectsOutOfWater.Contains(block.Rigi))
                _objectsOutOfWater.Remove(block.Rigi);
                
            if(!block.IsActive)
                block.IsActive = true;
        }
    }
}
