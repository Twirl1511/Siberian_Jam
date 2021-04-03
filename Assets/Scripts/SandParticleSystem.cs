using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandParticleSystem : MonoBehaviour
{
    [SerializeField] private GameObject _sandPartSystem;
    void Start()
    {
        
    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
        {
            GameObject sandParticles;
            Vector3 position = new Vector3(collision.contacts[0].point.x, -10f, 0);
            //if(block.RayCastLength <= 7)
            //{

            //}
            sandParticles = Instantiate(_sandPartSystem, position, Quaternion.Euler(-90,0,0));
            
            Destroy(sandParticles, 1);
        }
    }
}
