using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    private float scaleMultiplier = 1.5f;
    private SoundManager soundManager;
    private Vector3 originalScale;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance.GetComponent<GameManager>();
        
        soundManager = SoundManager.Instance.GetComponent<SoundManager>();
        originalScale = transform.localScale;
    }

    private void Update()
    {

        if (transform.localScale.x > originalScale.x && !gameManager._IsHoverOnBtn)
        {
            transform.localScale = originalScale;
        }
        
    }

    public void BigText()
    {

        gameManager._IsHoverOnBtn = true;

        ButtonHoverSound();

        if (transform.localScale.x < originalScale.x * scaleMultiplier)
        {
            transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier, transform.localScale.y * scaleMultiplier, transform.localScale.z * scaleMultiplier);
        }
    }

    public void SmallText()
    {
        gameManager._IsHoverOnBtn = false;
        transform.localScale = originalScale;
    }

    public void ButtonHoverSound()
    {
        soundManager.audioSource.PlayOneShot(soundManager.buttonHoverSfx);
    }
}
