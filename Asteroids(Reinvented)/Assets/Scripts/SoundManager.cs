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
    public AudioClip wormHoleSfx;
    public AudioClip buttonHoverSfx;
    public AudioClip buttonClickedSfx;
    public AudioClip componentPickupSfx;
    public AudioClip shipExplosionSfx;

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
