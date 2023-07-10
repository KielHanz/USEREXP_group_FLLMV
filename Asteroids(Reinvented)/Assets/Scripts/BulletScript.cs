using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletDespawnTime = 15f;
    public float bulletSpeed = 20f;

    private void Update()
    {
        DespawnBullet();
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }

    private void DespawnBullet()
    {
        bulletDespawnTime -= Time.deltaTime;
        if (bulletDespawnTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }
        Destroy(gameObject);
    }


}
