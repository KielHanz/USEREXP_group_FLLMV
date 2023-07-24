using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scraps : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float movementTimer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();
            playerScript.AddScraps(Random.Range(2, 6));
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(1f, 3f);
        movementTimer = 1f;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        if (movementTimer > 0)
        {
            movementTimer -= Time.deltaTime;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
