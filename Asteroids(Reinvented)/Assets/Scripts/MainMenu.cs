using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    CanvasGroup canvasGroup;
    private bool bgIsFading;
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSoundSfx;


    private void Start()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bgIsFading)
        {
            canvasGroup.alpha -= Time.deltaTime;
        }

        if (canvasGroup.alpha <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void StartGame()
    {
        Debug.Log("test");
        bgIsFading = true;
        audioSource.PlayOneShot(buttonSoundSfx);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
