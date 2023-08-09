using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    private bool isInWormHole;
    [SerializeField] private Image blackImage;
    private Color color;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.Instance.GetComponent<SoundManager>();    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            isInWormHole = true;
            soundManager.audioSource.PlayOneShot(soundManager.wormHoleSfx);
        }
    }

    private void Update()
    {
        if (isInWormHole)
        {
            if (blackImage.color.a < 1f)
            {
                color.a += Time.deltaTime;
                blackImage.color = color;
            }
            if (blackImage.color.a > 1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
        
}
