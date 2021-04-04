using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private AudioSource _audioSourse;
    [SerializeField] private Image _imageSound;
    [SerializeField] private Sprite _audioOff;
    [SerializeField] private Sprite _audioOnn;
    private bool _audioSwitch = false;
    public static bool IsPaused = false;
    [SerializeField] private GameObject _creditsPanel;

    void Start()
    {
        _creditsPanel.SetActive(false);
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
    

    public void OnCredits()
    {
        _creditsPanel.SetActive(true);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
    public void OnMusicSwitch()
    {
        _audioSwitch = !_audioSwitch;
        _audioSourse.mute = _audioSwitch;

        if (_audioSwitch)
        {
            _imageSound.sprite = _audioOff;
        }
        else
        {
            _imageSound.sprite = _audioOnn;
        }

        

    }

    public void OnBackToMenu()
    {
        _mainMenuPanel.SetActive(true);
        _pausePanel.SetActive(false);
        _resumeButton.SetActive(true);
        _pausePanel.SetActive(false);
        _creditsPanel.SetActive(false);
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
