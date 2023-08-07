using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StationaryAsteroid : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int rotationDirection;

    private AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>() != null || collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.asteroidBreakSfx);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(10f, 40f);
        RandomRotate();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    private void  RandomRotate()
    {
        rotationDirection = Random.Range(-1, 2);

        if (rotationDirection == 0)
        {
            RandomRotate();
        }
    }
}
