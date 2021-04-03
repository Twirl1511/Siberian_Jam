using UnityEngine.Playables;
using UnityEngine;

public class PlayerCutsceneController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _disableScripts;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private float _speed;
    private Rigidbody2D _rigi;
    private bool _enabled;
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
            transform.position = Vector3.Lerp
            //_rigi.MovePosition(Vector3.Lerp(transform.position, _startPosition.position, Time.deltaTime * _speed));
            if((transform.position - _startPosition.position).magnitude <= 0.1f)
            {
                _enabled = false;
                _director.Play();
            }
        }
    }
}
