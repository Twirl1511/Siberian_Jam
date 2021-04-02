using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIteraction : MonoBehaviour
{
    [SerializeField] private WaterUp Water;
    [SerializeField] private float _outOfWaterGravity;
    private List<Rigidbody2D> _objectsOutOfWater = new List<Rigidbody2D>();

    private void Start() 
    {
        InvokeRepeating(nameof(CheckConditions), 2f, 2f);
    }

    private void CheckConditions()
    {
        foreach(Rigidbody2D r in _objectsOutOfWater)
        {
            Block b = r.GetComponent<Block>();
            if(r.velocity.magnitude <= 0f && b.IsActive)
            {
                print("Victory");
                Water.Up();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Rigidbody2D rigi = collision.GetComponent<Rigidbody2D>();
            rigi.gravityScale *= _outOfWaterGravity;
            _objectsOutOfWater.Add(rigi);
        }
        if (collision.CompareTag("Bubble"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Block>(out Block block))
        {
            Rigidbody2D rigi = block.GetComponent<Rigidbody2D>();
            rigi.gravityScale /= _outOfWaterGravity;

            if(_objectsOutOfWater.Contains(rigi))
                _objectsOutOfWater.Remove(rigi);
                
            if(!block.IsActive)
                block.IsActive = true;
        }
    }
}
