using UnityEngine.Playables;
using UnityEngine;

public class PlayerCutsceneController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _disableScripts;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _anyBlockPos;
    private int _currentWaypoint = 0;
    private Vector3 _basePosition;
    private Rigidbody2D _rigi;
    private bool _enabled = false;
    private bool _moveCloseToTop = false;
    private bool _jumpOnBlock = false;
    private Vector3 _topPosition;

    private void Start() {
        _rigi = GetComponent<Rigidbody2D>();
    }

    public void TurnOn()
    {
        _enabled = true;
        _basePosition = transform.position;
        FindTopPos();
        foreach(var script in _disableScripts)
        {
            script.enabled = false;
        }
    }

    private void Update() 
    {
        if(_enabled) MoveOnWay();
        if(_moveCloseToTop) MoveCloseToTop();
        if(_jumpOnBlock) JumpOnBlock();
    }

    private void MoveOnWay()
    {
        transform.position += (_waypoints[_currentWaypoint].position -_basePosition).normalized * _speed * Time.deltaTime;
        Vector3 direction = (_waypoints[_currentWaypoint].position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        if((transform.position - _waypoints[_currentWaypoint].position).magnitude <= 0.2f)
        {
            _basePosition = transform.position;
            _currentWaypoint++;
            if(_currentWaypoint >= _waypoints.Length)
            {
                _enabled = false;
                _moveCloseToTop = true;
            }
        }
    }

    private void MoveCloseToTop()
    {
        Vector3 direction = (_topPosition - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);

        if((transform.position - _topPosition).magnitude <= 3f)
        {
            _moveCloseToTop = false;
            _jumpOnBlock = true;
        }
    }

    private void JumpOnBlock()
    {
        Vector3 direction = (_topPosition - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);

        if((transform.position - _topPosition).magnitude <= 3f)
        {
            _moveCloseToTop = false;
            _jumpOnBlock = true;
        }
    }

    private void FindTopPos()
    {
        RaycastHit[] hits = Physics.BoxCastAll(_anyBlockPos.position + Vector3.up * 10, new Vector3(20f, 2f, 1f), Vector3.down, Quaternion.LookRotation(Vector3.down), 50f);
        RaycastHit hit = hits[0];
        float topY = -100f;
        foreach(var h in hits)
        {
            if(h.transform.GetComponent<Block>() != null)
            {
                if(topY < h.point.y)
                {
                    hit = h;
                    topY = h.point.y;
                }
            }
        }
        _topPosition = hit.point;
    }
}
