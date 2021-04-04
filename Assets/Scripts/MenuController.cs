using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private AudioListener _audioListener;
    private bool _audioSwitch = true;
    public static bool IsPaused = false;

    void Start()
    {
        //Time.timeScale = 0;
        IsPaused = true;
        _pauseButton.SetActive(false);
        _mainMenuPanel.SetActive(true);
        _pausePanel.SetActive(false);
        _resumeButton.SetActive(false);

        if (PlayerPrefs.GetInt("key") == 1)
        {
            _mainMenuPanel.SetActive(false);
            _pauseButton.SetActive(true);
            Time.timeScale = 1;
            StartCoroutine(DelayPauseOff());
            PlayerPrefs.DeleteAll();
            WaterIteraction._objectsOutOfWater.Clear();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }
    public void OnStartGame()
    {
        if (_resumeButton.activeSelf)
        {
            PlayerPrefs.SetInt("key", 1);
            SceneManager.LoadScene(0);
        }
        _pauseButton.SetActive(true);
        _mainMenuPanel.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(DelayPauseOff());
        WaterIteraction._objectsOutOfWater.Clear();
        _player.SetActive(true);
    }
    

    public void OnExitGame()
    {
        Application.Quit();
    }
    public void OnMusicSwitch()
    {
        _audioSwitch = !_audioSwitch;
        _audioListener.enabled = _audioSwitch;
    }

    public void OnBackToMenu()
    {
        _mainMenuPanel.SetActive(true);
        _pausePanel.SetActive(false);
        _resumeButton.SetActive(true);
        _pausePanel.SetActive(false);
    }
    public void OnResume()
    {
        _mainMenuPanel.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(DelayPauseOff());
    }
    public void OnPause()
    {
        if (IsPaused)
        {
            _pausePanel.SetActive(false);
            _pauseButton.SetActive(true);
            Time.timeScale = 1;
            StartCoroutine(DelayPauseOff());
        }
        else
        {
            _pausePanel.SetActive(true);
            _pauseButton.SetActive(false);
            //Time.timeScale = 0;
            IsPaused = true;
        }
    }

    IEnumerator DelayPauseOff()
    {
        yield return new WaitForSeconds(0.1f);
        IsPaused = false;
    }

}
