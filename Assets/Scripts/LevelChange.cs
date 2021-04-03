using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelObjects;
    private GameObject _currentLevelObjects;

    [SerializeField] private GameObject[] _frames;

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

    }

}
