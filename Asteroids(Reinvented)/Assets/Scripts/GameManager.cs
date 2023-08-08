using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> _puzzlePieces;
    public bool _gameIsPaused;
    public bool _isControlsOpen;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _controlsMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void BeginRestartLevel()
    {
        Invoke("RestartLevel", 1f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        _gameIsPaused = !_gameIsPaused;

        if (_isControlsOpen)
        {
            _isControlsOpen = !_isControlsOpen;
            _controlsMenu.SetActive(_isControlsOpen);
        }

        if (_gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        _pauseMenu.SetActive(_gameIsPaused);
    }

    public void ResumeGame()
    {
        Pause();
    }

    public void OpenControls()
    {
        _controlsMenu.SetActive(true);
        _pauseMenu.SetActive(false);
        _isControlsOpen = true;
    }

    public void BackControls()
    {
        _controlsMenu.SetActive(false);
        _pauseMenu.SetActive(true);
        _isControlsOpen = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
