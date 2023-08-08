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
    private float distanceSub = 20; //Usual outside asteroids

    public bool inner;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    PlayerScript player = collision.GetComponent<PlayerScript>();
    //    if (player != null)
    //    {
    //        _enemyHP--;
    //        Destroy(gameObject);
    //    }

    //    BulletScript bullet = collision.GetComponent<BulletScript>();
    //    if(bullet != null)
    //    {
    //        _enemyHP--;
    //    }

    //    if (player == null && bullet == null)
    //    {

    //        BounceOnHit(collision);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            _enemyHP--;
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.asteroidBreakSfx);
            Destroy(gameObject);
        }

        BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
        if (bullet != null)
        {
            _enemyHP--;
        }

        if (player == null && bullet == null)
        {

            BounceOnHit(collision);
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
        inner = true;
        asteroidSize = Random.Range(0.3f, 1.3f); //Random.Range(0.3f, 3f);
        _enemyHP = 2 * asteroidSize;
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
    }

    private void Update()
    {
        if(_enemyHP <= 0)
        {
            SoundManager.Instance.audioSource.PlayOneShot(SoundManager.Instance.asteroidBreakSfx);
            Destroy(this.gameObject);
        }

        //Meant for asteroids inside level
        if(inner == true)
        {
            distanceSub = 30;
        }

        if(Mathf.Abs(this.transform.position.x - player.transform.position.x) >= distanceSub ||
            Mathf.Abs(this.transform.position.y - player.transform.position.y) >= distanceSub) // both 20
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    public void TakeDamage()
    {
        _enemyHP--;
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

    private float GetAngle()
    {
        float angle = Mathf.Atan2(travelDirection.y, travelDirection.x) * Mathf.Rad2Deg;
        return angle;
    }

    //private void BounceOnHit()
    //{

    //    if (Mathf.Abs(travelDirection.x) > Mathf.Abs(travelDirection.y))
    //    {
    //        travelDirection = new Vector3(-travelDirection.x, travelDirection.y, 0);
    //    }
    //    if (Mathf.Abs(travelDirection.y) > Mathf.Abs(travelDirection.x))
    //    {
    //        travelDirection = new Vector3(travelDirection.x, -travelDirection.y, 0);
    //    }
    //}

    // this function is still weird.. work in progress... if you guys know any other codes that makes the astreoid reflect upon colliding with other game objects
    // such as walls go ahead and change thank you!
    //private void BounceOnHit(Collider2D other)
    //{
       
    //    Vector2 collisionNormal = (transform.position - other.transform.position).normalized;
    //    Vector2 reflectedDirection = Vector2.Reflect(travelDirection, collisionNormal);
    //    travelDirection = reflectedDirection.normalized;
    //}

    //I think this works?
    private void BounceOnHit(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal;
        Vector2 reflectedDirection = Vector2.Reflect(travelDirection, collisionNormal);
        travelDirection = reflectedDirection.normalized;
    }
}
