using UnityEngine.Playables;
using UnityEngine;
using DG.Tweening;

public class PlayerCutsceneController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _disableScripts;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float[] _speed;
    [SerializeField] private Transform _anyBlockPos;
    [SerializeField] private CameraFollow _cameraFollow;
    private int _currentWaypoint = 0;
    private Vector3 _basePosition;
    private Rigidbody2D _rigi;
    private bool _enabled = false;
    private bool _moveCloseToTop = false;
    private Vector3 _topPosition;

    private Sequence sequence;

    private void Start() {
        _rigi = GetComponent<Rigidbody2D>();
        DOTween.Init();
        sequence = DOTween.Sequence();
    }

    public void TurnOn()
    {
        _cameraFollow.Enable();
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
    }

    private void MoveOnWay()
    {
        if(_enabled)
        {
            transform.position += (_waypoints[_currentWaypoint].position -_basePosition).normalized * _speed[_currentWaypoint] * Time.deltaTime;
            Vector3 direction = (_waypoints[_currentWaypoint].position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);

            if((transform.position - _waypoints[_currentWaypoint].position).magnitude <= 0.2f)
            {
                _basePosition = transform.position;
                _currentWaypoint++;
                if(_currentWaypoint >= _waypoints.Length)
                {
                    _currentWaypoint--;
                    _enabled = false;
                    _moveCloseToTop = true;
                }
            }
        }
    }

    private void MoveCloseToTop()
    {
        Vector3 direction = (_topPosition - transform.position).normalized;

        transform.position += direction * 0.02f;
        transform.rotation = Quaternion.LookRotation(direction);

        if((transform.position - _topPosition).magnitude <= 4f)
        {
            _moveCloseToTop = false;
            JumpOnBlock();
        }
    }

    private void JumpOnBlock()
    {
        sequence
            .Append(transform.DOJump(_topPosition + Vector3.up * 0.5f, 1f, 1, 1f)
            .Append(transform.DOJump(transform.position + Vector3.up * 4f + Vector3.right * 3f, 1f, 1, 1f)
            .OnComplete(EnableEndVideo)));
    }

    private void EnableEndVideo()
    {
        sequence.Kill();
        Destroy(gameObject);
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
        _topPosition.z = transform.position.z;
    }
}
