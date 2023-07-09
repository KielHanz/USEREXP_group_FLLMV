using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]private float _enemyHP;
    private Vector2 randomDir;
    private GameObject player;
    private Vector3 direction;
    private Vector3 travelDirection;
    private float speed;
    private float asteroidSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript player = collision.GetComponent<PlayerScript>();
        if (player != null)
        {
            _enemyHP--;
        }
        BulletScript bullet = collision.GetComponent<BulletScript>();
        if(bullet != null)
        {
            _enemyHP--;
        }
    }

    private void Start()
    {
        randomDir = Random.insideUnitCircle.normalized;
        player = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(2.5f, 6.5f);
        direction = player.transform.position - this.transform.position;
        travelDirection = direction + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        travelDirection.Normalize();
        asteroidSize = Random.Range(0.3f, 3f);
        _enemyHP = 2 * asteroidSize;
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
    }

    private void Update()
    {
        if(_enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }

        if(Mathf.Abs(this.transform.position.x - player.transform.position.x) >= 20 ||
            Mathf.Abs(this.transform.position.y - player.transform.position.y) >= 20)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void RandomMove()
    {

        transform.Translate(randomDir.x * Time.deltaTime * 2, randomDir.y * Time.deltaTime * 2, 0);
        Debug.Log(randomDir);
    }

    private void MoveTowardsPlayer()
    {
        transform.Translate(travelDirection * Time.deltaTime * speed);
    }
}
