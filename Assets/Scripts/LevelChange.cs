using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelObjects;
    private GameObject _currentLevelObjects;
    [SerializeField] private GameObject[] _frames;
    [SerializeField] private float _timeBtwFrames;

    void Start()
    {
        WaterUp.singeton.ChangeLevel += OnRestart;
        _currentLevelObjects = Instantiate(_levelObjects[0]);
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
        for(int i = 0; i < _frames.Length; i++)
        {
            yield return new WaitForSeconds(seconds);
            _frames[i].SetActive(true);
        }
        SoundManager.singleton.PlaySoud(SoundManager.singleton.WaterUp);
        // повышается уровень воды
        /// разрушается башня - отдельный скрипт с течением
        /// // исчезает рука с кувшином
        /// отключаются все сцены
        /// затемнение
        /// затемнение прошло
        /// cпавнятся объекты
        /// текст над рыбкой
        /// MenuController.IsPaused = false;
    }
}
