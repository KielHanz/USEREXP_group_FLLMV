using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StationaryAsteroid : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int rotationDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>() != null)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
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
