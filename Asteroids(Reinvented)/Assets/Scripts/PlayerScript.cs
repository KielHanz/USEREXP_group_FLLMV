using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] public float m_Speed = 5f;
    [SerializeField] Rigidbody2D playerBody;
    public bool isAlive;
    private float _maxHp;
    public float _playerHP;
    public int _playerAmmo = 30;
    public Image healthBar;
    public TMP_Text playerAmmoUI;
    public GameObject bulletObject;

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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    EnemyScript enemy = collision.GetComponent<EnemyScript>();
    //    if (enemy != null)
    //    {
    //        PlayerTakeDmg(1);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            PlayerTakeDmg(1);
        }
    }

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        isAlive = true;
        canShoot = false;
        _maxHp = 3;
        _playerHP = _maxHp;
        playerAmmoUI.text = "Ammo: " + _playerAmmo.ToString();
    }

    private void Update()
    {

        GetMousePos();
        if (_playerAmmo > 0)
        {
            ShootWeapon();
        }

        if (shootTimer <= 0)
        {
            canShoot = true;
        }
        else
        {
            shootTimer -= Time.deltaTime;
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
        healthBar.fillAmount = _playerHP / _maxHp;

        if (_playerHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ShootWeapon()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (canShoot == true)
            {
                bulletObject = Instantiate(bulletGameObject, bulletTransform.position, bulletTransform.rotation);
                shootTimer = timeToShoot;
                canShoot = false;
                _playerAmmo--;

                playerAmmoUI.text = "Ammo: " + _playerAmmo.ToString();

                Debug.Log("Ammo Remaining: " + _playerAmmo);
            }
        }
    }
}
