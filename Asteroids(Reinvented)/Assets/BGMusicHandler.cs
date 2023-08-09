using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicHandler : MonoBehaviour
{
    [SerializeField] private GameObject bgLevelMusic;
    [SerializeField] private GameObject bgGameOverMusic;

    private AudioSource levelAudioSource;
    private AudioSource gameOverAudioSource;

    private PlayerScript player;
    private bool gameOverTriggered = false;

    private void Start()
    {
        player = GameManager.Instance.GetComponent<GameManager>()._player;

        levelAudioSource = bgLevelMusic.GetComponent<AudioSource>();
        gameOverAudioSource = bgGameOverMusic.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameOverTriggered && player._playerHP <= 0)
        {
            levelAudioSource.Stop();
            gameOverAudioSource.Play();
            gameOverTriggered = true;
        }
    }
}
