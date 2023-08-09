using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerScript _player;
    public List<GameObject> _puzzlePieces;
    public bool _gameIsPaused;
    public bool _isControlsOpen;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _gameOverMenu;

    public Image blackImage;
    public Color color;

    private CanvasGroup gameOverCanvasGroup;

    public bool _bgIsFading;
    public bool _IsHoverOnBtn;

    private SoundManager soundManager;

    [SerializeField]private bool isDisabled;

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

        if (isDisabled) return;

        gameOverCanvasGroup = _gameOverMenu.GetComponent<CanvasGroup>();
        soundManager = GetComponent<SoundManager>();
        _bgIsFading = true;
        color.a = blackImage.color.a;
        blackImage.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isDisabled) return;

        if (_bgIsFading && color.a > 0)
        {
            color.a -= Time.deltaTime;
            blackImage.color = color;
        }
        else if (color.a <= 0)
        {
            _bgIsFading = false;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
            _IsHoverOnBtn = false;
        }

        if (_player != null)
        {
            if (_player._playerHP <= 0 && gameOverCanvasGroup.alpha < 1f)
            {
                Invoke("ShowGameOverCanvas", 2f);
            }
        }
    }

    public void BeginRestartLevel()
    {
        Time.timeScale = 0.25f;
        soundManager.audioSource.PlayOneShot(soundManager.buttonClickedSfx);
        Invoke("RestartLevel", 0.25f);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        soundManager.audioSource.PlayOneShot(soundManager.buttonClickedSfx);
        Pause();
    }

    public void OpenControls()
    {
        _IsHoverOnBtn = false;
        soundManager.audioSource.PlayOneShot(soundManager.buttonClickedSfx);
        _controlsMenu.SetActive(true);
        _pauseMenu.SetActive(false);
        _isControlsOpen = true;
    }

    public void BackControls()
    {
        _IsHoverOnBtn = false;
        soundManager.audioSource.PlayOneShot(soundManager.buttonClickedSfx);
        _controlsMenu.SetActive(false);
        _pauseMenu.SetActive(true);
        _isControlsOpen = false;
    }

    public void ExitGame()
    {
        soundManager.audioSource.PlayOneShot(soundManager.buttonClickedSfx);
        Application.Quit();
    }

    public void ShowGameOverCanvas()
    {
        _gameOverMenu.SetActive(true);
        gameOverCanvasGroup.alpha += (Time.deltaTime / 2f);
    }

    public void InitializePlayer(PlayerScript player)
    {
        _player = player;
    }
}
