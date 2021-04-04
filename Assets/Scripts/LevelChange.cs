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
        }
    }


    public void OnRestart()
    {
        WaterIteraction._objectsOutOfWater.Clear();
        Destroy(_currentLevelObjects);
        _currentLevelObjects = Instantiate(_levelObjects[WaterUp._currentLevelIndex + 1]);
        //StartCoroutine(FirstCloud(1));
        //if(WaterUp._currentLevelIndex + 1 < 3)
        //{
        //    ShowCloud();
        //}
        //else
        //{
        //    MenuController.IsPaused = false;
        //}
        
    }

    public void NextLevelAnimation()
    {
        MenuController.IsPaused = true;
        StartCoroutine(SceneSwitcher(1));
    }

    public void ShowCloud()
    {
        StartCoroutine(FirstCloud(1));
    }
    IEnumerator FirstCloud(float sec)
    {
        
        int cloudNumber = WaterUp._currentLevelIndex + 1;
        yield return new WaitForSeconds(sec);
        _clouds[cloudNumber].SetActive(true);
        MenuController.IsPaused = true;
        yield return new WaitForSeconds(4);
        _clouds[cloudNumber].SetActive(false);
        MenuController.IsPaused = false;
    }

    IEnumerator SceneSwitcher(float seconds)
    {
        // ������������������ ������ ��� ������� ������ � �������� ����
        for (int i = 0; i < _frames.Length; i++)
        {
            yield return new WaitForSeconds(seconds);
            _frames[i].SetActive(true);
        }
        DestroyTower();
        yield return new WaitForSeconds(0.5f);
        //������ ���� ����
        SoundManager.singleton.PlaySoud(SoundManager.singleton.WaterUp);
        // ��������� ����
        WaterUp.singeton.WaterUpPush();
        // ��������� �����
        
        
        yield return new WaitForSeconds(2);
        
        /// ������� ������� ������ ����
        _fadePanel.TurnOn(null);
        /// ������� ������� �� �����
        yield return new WaitForSeconds(1);
        for (int i = 0; i < _frames.Length; i++)
        {
            _frames[i].SetActive(false);
        }
        _fadePanel.TurnOff(null);
        /// ������� ����� ������� �� �����
        
        OnRestart();
        ShowCloud();
    }

    private void DestroyTower()
    {
        _isWaveActive = true;
    }
}
