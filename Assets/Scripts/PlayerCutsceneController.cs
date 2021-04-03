using UnityEngine.Playables;
using UnityEngine;

public class PlayerCutsceneController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _disableScripts;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private float _moveToStartTime;
    private Rigidbody2D _rigi;
    private bool _enabled;
    private float _time = 0f;
    private Vector3 _basePosition;

    private void Start() {
        _rigi = GetComponent<Rigidbody2D>();
    }

    public void TurnOn()
    {
        _enabled = true;
        _basePosition = transform.position;
        foreach(var script in _disableScripts)
        {
            script.enabled = false;
        }
    }

    private void Update() 
    {
        if(_enabled)
        {
            _time += Time.deltaTime;
            transform.position = Vector3.Lerp(_basePosition, _startPosition.position, _time /_moveToStartTime);
            //_rigi.MovePosition(Vector3.Lerp(transform.position, _startPosition.position, Time.deltaTime * _speed));
            if((transform.position - _startPosition.position).magnitude <= 0.1f)
            {
                print(111);
                _enabled = false;
                _director.Play();
            }
        }
    }
}
