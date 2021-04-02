using UnityEngine;

public class WaterUp : MonoBehaviour
{

    [SerializeField] private float _speed;
    public Transform[] Levels;
    private Transform _currentLevel;
    private int _currentLevelIndex = -1;
    private float _startPosition;
    private float _testPos;

    public void Up()
    {
        _currentLevelIndex++;
        _currentLevel = Levels[_currentLevelIndex];
        _startPosition = transform.position.y;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Up();
        }

        if(_currentLevel != null)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
            if(transform.position.y >= _currentLevel.position.y)
            {
                _currentLevel = null;
            }
        }
    }
}
