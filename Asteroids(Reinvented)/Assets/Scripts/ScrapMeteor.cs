using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScrapMeteor : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int rotationDirection;
    [SerializeField] private GameObject scrapPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>() != null)
        {
            int scrapDropCount = Random.Range(1, 4);
            for (int i = 0; i < scrapDropCount; i++)
            {
                Instantiate(scrapPrefab, transform.position, Quaternion.identity);
            }
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.scrapBreakSfx);
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.scrapBreakSfx);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(10f, 40f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
