using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelObjects;
    private GameObject _currentLevelObjects;
    [SerializeField] private GameObject[] _frames;
    [SerializeField] private float _timeBtwFrames;
    [SerializeField] private float _forceDestroyTower;

    [SerializeField] private GameObject _waveGameObject;
    private Vector3 _waveStartPosition;
    [SerializeField] private GameObject _waveEndPositionGameObject;
    private Rigidbody _waveRb;
    [SerializeField] private float _waveSpeed;
    private Vector3 _waveEndPosition;
    private bool _isWaveActive = false;


    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private FadePanel _fadePanel;

    [SerializeField] private GameObject[] _clouds;

    void Start()
    {
        _waveRb = _waveGameObject.GetComponent<Rigidbody>();
        _waveEndPosition = _waveEndPositionGameObject.transform.position;
        _waveStartPosition = _waveGameObject.transform.position;
        WaterUp.singeton.ChangeLevel += NextLevelAnimation;
        _currentLevelObjects = Instantiate(_levelObjects[0]);
    }

    private void FixedUpdate()
    {
        ActivateWave();
    }

    private void ActivateWave()
    {
        if (_isWaveActive)
        {
            if (_waveGameObject.transform.position.x <= _waveEndPosition.x)
            {
                _isWaveActive = false;
                _waveGameObject.transform.position = _waveStartPosition;
                _waveRb.velocity = Vector3.zero;
            }
            _waveRb.AddForce(Vector3.left * _waveSpeed * Time.deltaTime);
            //_waveGameObject.transform.position = Vector3.MoveTowards(_waveGameObject.transform.position, _waveEndPosition, _waveSpeed * Time.deltaTime);
        }
    }

    private void BackScreening()
    {
        //_blackScreenSpriteRenderer.
    }


    public void OnRestart()
    {
        WaterIteraction._objectsOutOfWater.Clear();
        //Destroy(_levelObjects[WaterUp._currentLevelIndex]); 
        Destroy(_currentLevelObjects);
        _currentLevelObjects = Instantiate(_levelObjects[WaterUp._currentLevelIndex + 1]);
    }

    public void NextLevelAnimation()
    {
        MenuController.IsPaused = true;
        StartCoroutine(SceneSwitcher(_timeBtwFrames));
    }

    


    IEnumerator SceneSwitcher(float seconds)
    {
        // последовательность кадров как девочка встает и наливает воду
        for (int i = 0; i < _frames.Length; i++)
        {
            yield return new WaitForSeconds(seconds);
            _frames[i].SetActive(true);
        }
        DestroyTower();
        yield return new WaitForSeconds(0.5f);
        //льется вода звук
        SoundManager.singleton.PlaySoud(SoundManager.singleton.WaterUp);
        // поднимаем воду
        WaterUp.singeton.WaterUpPush();
        // разрушаем башню
        
        
        yield return new WaitForSeconds(2);
        
        
        
        /// спавним объекты нового лвла
        _fadePanel.TurnOn(null);
        /// девочка садится на место
        yield return new WaitForSeconds(1);
        for (int i = 0; i < _frames.Length; i++)
        {
            _frames[i].SetActive(false);
        }
        _fadePanel.TurnOff(null);
        /// спавним новые объекты на левле
        OnRestart();
        MenuController.IsPaused = false;

        /// текст над рыбкой
        /// MenuController.IsPaused = false;
    }

    private void DestroyTower()
    {
        _isWaveActive = true;
    }
}
