using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIteraction : MonoBehaviour
{
    [SerializeField] private float _outOfWaterGravity;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            collision.GetComponent<Rigidbody2D>().gravityScale *= _outOfWaterGravity;
        }
        if (collision.CompareTag("Bubble"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            collision.GetComponent<Rigidbody2D>().gravityScale /= _outOfWaterGravity;
        }
    }
}
