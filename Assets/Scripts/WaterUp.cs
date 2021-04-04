using UnityEngine;
using DG.Tweening;
using System.Collections;

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
    private bool _inMove;

    private void Awake()
    {
        singeton = this;
    }

    public void Up()
    {
        if(enabled == false)
            return;
        if(!_inMove)
        {
            _currentLevelIndex++;
            if(_currentLevelIndex >= Levels.Length)
            {
                _endSceneController.TurnOn();
                enabled = false;
            }
            else
            {
                _inMove = true;
                _currentLevel = Levels[_currentLevelIndex];
                ChangeLevel?.Invoke();
                //transform.DOMoveY(_currentLevel.transform.position.y, _speed).OnComplete(ResetMove);
                /// сделал костыль через куротину, пока не придумал как иначе
                /// чтобы вода поднималась после смены картинок
                //StartCoroutine(DelayWaterUp(4));
            }
        }
        else
        {
            _currentLevel = Levels[_currentLevelIndex];
            //ChangeLevel?.Invoke();
        }
    }

    /// сделал костыль через куротину, пока не придумал как иначе
    //IEnumerator DelayWaterUp(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    transform.DOMoveY(_currentLevel.transform.position.y, _speed).OnComplete(ResetMove);
    //}
    public void WaterUpPush()
    {
        transform.DOMoveY(_currentLevel.transform.position.y, _speed).OnComplete(ResetMove);
    }
    

    private void ResetMove()
    {
        _inMove = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Up();
        }
    }

    
}
