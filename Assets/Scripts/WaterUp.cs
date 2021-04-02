using UnityEngine;

public class WaterUp : MonoBehaviour
{

    [SerializeField] private float _speed;
    public Transform[] Levels;
    private Transform _currentLevel;
    private int _currentLevelIndex = 0;
    private Vector3 _startPosition;

    public void Up()
    {
        _currentLevelIndex++;
        _currentLevel = Levels[_currentLevelIndex];
        _startPosition = transform.position;
    }

    private void Update()
    {
        if(_currentLevel != null)
        {
            transform.position = Vector3.Lerp(_startPosition, _currentLevel.position, Time.deltaTime * _speed);
        }
        
    }


}
