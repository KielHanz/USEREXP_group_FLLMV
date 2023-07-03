using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]private int _enemyHP;
    private Vector2 randomDir;

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
        _enemyHP = 1;
        randomDir = Random.insideUnitCircle.normalized;

    }

    private void Update()
    {
        if(_enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
        RandomMove();
    }

    private void RandomMove()
    {

        transform.Translate(randomDir.x * Time.deltaTime, randomDir.y * Time.deltaTime, 0);
        Debug.Log(randomDir);
    }
}
