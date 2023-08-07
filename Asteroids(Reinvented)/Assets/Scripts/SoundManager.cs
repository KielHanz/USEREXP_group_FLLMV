using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip shootSfx;
    public AudioClip asteroidBreakSfx;
    public AudioClip scrapBreakSfx;
    public AudioClip doorSfx;
    public AudioClip pickUpSfx;
    public AudioClip craftSfx;
    public AudioClip repairSfx;

    public AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }
}
