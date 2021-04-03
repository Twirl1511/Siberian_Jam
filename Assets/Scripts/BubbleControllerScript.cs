using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleControllerScript : MonoBehaviour
{
    [SerializeField] private Transform BubbleJoint;
    [SerializeField] private Bubble BubblePrefab;
    [SerializeField] private float _defaultSize;
    [SerializeField] private float _defaultLifeTime;
    [SerializeField] private float _defaultDrag;
    [SerializeField] private float _increaseSizePerSecond;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private float _additionalVelocity;
    private float x;
    private float y;
    private float z;
    private Bubble _bubbleScript;
    private float lifeTime;
    public AnimationCurve GravityCurve;

    [SerializeField] private float _timeToCreateBubble;
    private float _time;
    private bool _flag;
    [HideInInspector] public static List<Bubble> Bubbles = new List<Bubble>();

    void Start()
    {
        _flag = true;
        _bubbleScript = BubblePrefab.GetComponent<Bubble>();
        SetDefaultParameters();
    }

    void Update()
    {
        if (!MenuController.IsPaused)
        {
            CreateBubble();
            IncreaseBubbleSize();
        }
    }

    public void SetDefaultParameters()
    {
        x = _defaultSize;
        y = _defaultSize;
        z = _defaultSize;
        lifeTime = _defaultLifeTime;
    }

    //public void SetParameters()
    //{
    //    _bubbleScript.gravityScale = -GravityCurve.Evaluate(x) * _gravityModifier;
    //    lifeTime = 10;
    //}
    public void SetParameters(Bubble bubble)
    {
        bubble.gravityScale = -GravityCurve.Evaluate(x) * _gravityModifier;
        lifeTime = 10;
    }

    private void Spawn()
    {
        Bubble bubble = Instantiate(BubblePrefab, (Vector2)BubbleJoint.position, Quaternion.identity);
        bubble.transform.localScale = new Vector3(x,y,z);
        SetParameters(bubble);
        bubble.OnDestroyEvent += OnBubbleHlopHlop;
        Bubbles.Add(bubble);

        Destroy(bubble, lifeTime);
    }

    private void OnBubbleHlopHlop(Bubble bubble)
    {
        bubble.OnDestroyEvent -= OnBubbleHlopHlop;
        Bubbles.Remove(bubble);
    }

    private void CreateBubble()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //SetParameters();
            if (_flag)
            {
                Spawn();
                _time = 0;
            }

            SetDefaultParameters();
            _flag = true;
        }
    }

    private void IncreaseBubbleSize()
    {
        if (Input.GetMouseButton(0) && _flag)
        {
            _time += Time.deltaTime;
            x += _increaseSizePerSecond * Time.deltaTime;
            y += _increaseSizePerSecond * Time.deltaTime;
            z += _increaseSizePerSecond * Time.deltaTime;
            //BubblePrefab.transform.localScale = new Vector3(x, y, z);

            if (_time >= _timeToCreateBubble)
            {
                Spawn();
                SetDefaultParameters();
                _flag = false;
                _time = 0;
            }
        }
    }
}
