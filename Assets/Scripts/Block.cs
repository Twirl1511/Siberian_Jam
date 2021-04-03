using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsActive = true;
    [HideInInspector] public Rigidbody Rigi;

    private void Start() {
        Rigi = GetComponent<Rigidbody>();
    }
}
