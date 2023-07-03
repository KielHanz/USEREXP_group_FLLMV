using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] public float m_Speed = 5f;
    [SerializeField] Rigidbody2D playerBody;
    public bool isAlive;
    public int _playerHP;
    public int _playerAmmo = 30;

    #region Player Addons
    [SerializeField] private Camera mainCamera;
    [SerializeField] Transform bulletTransform;
    [SerializeField] GameObject bulletGameObject;
    public float shootTimer = .5f;
    public float timeToShoot = .5f;
    private bool canShoot;
    #endregion

    #region Player Smooth Movement
    [SerializeField] public float accel;
    [SerializeField] public float deaccel;
    [SerializeField] private float maxSpeed;
    private float currentSpeed;
    private bool isMoving;
    //[SerializeField] public float currentForwardDir;
    private Vector3 lastVelocity;
    private Vector2 movementVector;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if(enemy != null)
        {
            PlayerTakeDmg(1);
        }
    }

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        isAlive = true;
        canShoot = false;
        _playerHP = 3;
    }

    private void Update()
    {

        GetMousePos();
        if(_playerAmmo > 0)
        {
            playerShoot();
        }
        
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            canShoot = true;
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
      

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(inputX, inputY);

        //check if player is moving
        if (Mathf.Abs(inputX) > 0 || Mathf.Abs(inputY) > 0)
        {
            isMoving = true;
        }
        else if (inputX == 0 || inputY == 0)
        {
            isMoving = false;
        }

        //maxspeed
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        //accelerate player
        if (isMoving)
        {
            
            currentSpeed += accel * Time.deltaTime;
            playerBody.velocity = input * currentSpeed * Time.deltaTime;

        }
        else
        {

            currentSpeed = 0;
            playerBody.velocity -= playerBody.velocity * 0.1f * Time.deltaTime;

        }



        //Vector3 pos = transform.position;
        //if (Input.GetKey(KeyCode.W))
        //{
        //    pos.y += currentSpeed *  Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    pos.y -= currentSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    pos.x += currentSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    pos.x -= currentSpeed * Time.deltaTime;
        //}
        //transform.position = pos;
    }


    public void GetMousePos()
    {
       // Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
    }

    public void PlayerTakeDmg(int dmg)
    {
        _playerHP -= dmg;
        if(_playerHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void playerShoot()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (canShoot == true)
            {
                Instantiate(bulletGameObject, bulletTransform.position, bulletTransform.rotation);
                shootTimer += timeToShoot;
                canShoot = false;
                _playerAmmo--;
            }
        }
    }
}
