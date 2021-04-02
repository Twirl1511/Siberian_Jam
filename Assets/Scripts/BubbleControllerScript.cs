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
    float x;
    float y;
    float z;
    Rigidbody2D rb;
    float lifeTime;


    void Start()
    {
        rb = BubblePrefab.GetComponent<Rigidbody2D>();
        SetDefaultParameters();
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (x > 0.7f)
            {
                rb.drag = 5;
                lifeTime = 2;
            }
            

            //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    //public void SetParameters()
    //{
    //    if (x > 0.7f)
    //    {
    //        rb.drag = 5;
    //        lifeTime = 2;
    //    }
    //}
}
