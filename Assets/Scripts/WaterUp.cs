using UnityEngine;

public class WaterUp : MonoBehaviour
{
    public static WaterUp singeton;

    [SerializeField] private PlayerCutsceneController _endSceneController;
    [SerializeField] private float _speed;
    public Transform[] Levels;
    private Transform _currentLevel;
    public static int _currentLevelIndex = -1;
    public delegate void Action();
    public event Action ChangeLevel;

    private void Awake()
    {
        singeton = this;
    }
    public void Up()
    {
        _currentLevelIndex++;
        if(_currentLevelIndex >= Levels.Length)
        {
            _endSceneController.TurnOn();
            enabled = false;
        }
        else
        {
            _currentLevel = Levels[_currentLevelIndex];
            ChangeLevel?.Invoke();
        }

        
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
