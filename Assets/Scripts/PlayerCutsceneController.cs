using UnityEngine;

public class PlayerCutsceneController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _disableScripts;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed;
    private Rigidbody _rigi;
    private bool _enabled;
    private Vector3 _basePosition;

    private void Start() {
        _rigi = GetComponent<Rigidbody>();
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
            _rigi.MovePosition(Vector3.Lerp(_basePosition, _startPosition.position, Time.deltaTime * _speed));
        }
    }
}
