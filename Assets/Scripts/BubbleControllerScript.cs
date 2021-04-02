using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleControllerScript : MonoBehaviour
{
    [SerializeField] private Transform BubbleJoint;
    [SerializeField] private GameObject BubblePrefab;
    [SerializeField] private float _defaultSize;
    [SerializeField] private float _defaultLifeTime;
    [SerializeField] private float _defaultDrag;
    [SerializeField] private float _increaseSizePerSecond;
    [SerializeField] private float _gravityModifier;
    float x;
    float y;
    float z;
    Rigidbody2D rb;
    float lifeTime;
    public AnimationCurve GravityCurve;

    void Start()
    {
        rb = BubblePrefab.GetComponent<Rigidbody2D>();
        SetDefaultParameters();
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //if (x > 0.7f)
            //{
            //    rb.drag = 5;
            //    lifeTime = 2;
            //}
            SetParameters();

            Destroy(Instantiate(BubblePrefab, (Vector2)BubbleJoint.position, Quaternion.identity), lifeTime);
            SetDefaultParameters();
            
        }

        if (Input.GetMouseButton(0))
        {
            x += _increaseSizePerSecond * Time.deltaTime;
            y += _increaseSizePerSecond * Time.deltaTime;
            z += _increaseSizePerSecond * Time.deltaTime;
            BubblePrefab.transform.localScale = new Vector3(x,y,z);
        }

    }
    public void SetDefaultParameters()
    {
        x = _defaultSize;
        y = _defaultSize;
        z = _defaultSize;
        lifeTime = _defaultLifeTime;
        rb.drag = _defaultDrag;
    }

    public void SetParameters()
    {
        //Easing.Evaluate(x);
        //rb.gravityScale = -_gravityScale * x;
        rb.gravityScale = -GravityCurve.Evaluate(x) * _gravityModifier;
        lifeTime = 10;
    }
}
