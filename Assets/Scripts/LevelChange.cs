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
    private SpriteRenderer _blackScreenSpriteRenderer;

    void Start()
    {
        _waveRb = _waveGameObject.GetComponent<Rigidbody>();
        _blackScreenSpriteRenderer = _blackScreen.GetComponent<SpriteRenderer>();
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
        SoundManager.singleton.PlaySoud(SoundManager.singleton.WaterUp);
        // поднимаем воду
        WaterUp.singeton.WaterUpPush();
        // разрушаем башню
        DestroyTower();
        yield return new WaitForSeconds(3);
        
        for (int i = 0; i < _frames.Length; i++)
        {
            _frames[i].SetActive(false);
        }
        // возвращаем игроку движение
        MenuController.IsPaused = false;
        /// затемнение
        /// затемнение прошло
        /// cпавнятся объекты
        /// текст над рыбкой
        /// MenuController.IsPaused = false;
    }

    private void DestroyTower()
    {

        //_invisibleForceGameObject.transform.Translate(_invisibleForceEndPosition.transform.position,);

        _isWaveActive = true;









        //Vector3 direction;
        //if (_levelObjects[counter].transform.GetChild(0).transform.position.x >= 0)
        //{
        //    direction = Vector3.left;
        //}
        //else
        //{
        //    direction = Vector3.right;
        //}

        //print(_levelObjects[counter].transform.childCount);
        //for (int i = 0; i < _levelObjects[counter].transform.childCount; i++)
        //{
        //    //_levelObjects[counter].transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
        //    //testRosck.GetComponent<Rigidbody>().AddForce(Vector3.left * 1000);
        //}
    }
}
