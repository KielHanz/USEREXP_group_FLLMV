using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] public float m_Speed = 5f;
    [SerializeField] Rigidbody2D playerBody;
    public bool isAlive;
    public float _maxHp;
    public float _playerHP;
    public int _playerAmmo = 30;
    public int _puzzlePiecesCollected;
    public Image healthBar;
    public TMP_Text playerAmmoUI;
    public TextMeshProUGUI _puzzlePiecesText;

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

    [SerializeField] private GameObject scrapMenu;
    private bool isScrapMenuOpen = false;

    public int scrapCount;
    public TextMeshProUGUI scrapCountText;

    private SoundManager soundManager;
    private AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            PlayerTakeDmg(1);
        }

        if (collision.gameObject.GetComponent<StationaryAsteroid>() != null)
        {
            PlayerTakeDmg(1);
        }
    }

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();

        _puzzlePiecesCollected = 0;
        isAlive = true;
        canShoot = false;
        _maxHp = 3;
        _playerHP = _maxHp;
        playerAmmoUI.text = "Ammo: " + _playerAmmo.ToString();
        scrapCount = 0;
        scrapCountText.SetText(scrapCount.ToString());

        soundManager = SoundManager.Instance.GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();

        if (_puzzlePiecesText != null)
        {
            Invoke("SetInitialPuzzlePiecesCount", 0.5f);
        }
    }

    private void Update()
    {
        if (GameManager.Instance._gameIsPaused)
        {
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleMenu();
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
            //Destroy(this.gameObject);
            GameManager.Instance.BeginRestartLevel();
            this.gameObject.SetActive(false);
        }
    }

    public void ShootWeapon()
    {
        if (!IsMouseOverScrapMenu())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot == true)
            {
                Instantiate(bulletGameObject, bulletTransform.position, bulletTransform.rotation);
                shootTimer = timeToShoot;
                canShoot = false;
                _playerAmmo--;

                playerAmmoUI.text = "Ammo: " + _playerAmmo.ToString();

                Debug.Log("Ammo Remaining: " + _playerAmmo);

                audioSource.PlayOneShot(soundManager.shootSfx);

            }
        }
    }

    public void AddScraps(int amount)
    {
        scrapCount += amount;
        scrapCountText.SetText(scrapCount.ToString());
    }

    public void DeductScraps(int amount)
    {
        scrapCount -= amount;
        scrapCount = Mathf.Max(scrapCount, 0);
        scrapCountText.SetText(scrapCount.ToString());
    }

    public void AddAmmo(int amount)
    {
        _playerAmmo += amount;
        playerAmmoUI.text = "Ammo: " + _playerAmmo.ToString();
    }

    public void Heal(int amount)
    {
        _playerHP += amount;
        _playerHP = Mathf.Min(_playerHP, _maxHp);
        healthBar.fillAmount = _playerHP / _maxHp;
    }

    private void ToggleMenu()
    {
        isScrapMenuOpen = !isScrapMenuOpen;
        scrapMenu.SetActive(isScrapMenuOpen);
    }

    private bool IsMouseOverScrapMenu()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<UIElement>() != null)
            {
                return false;
            }
        }

        return true;
    }

    public void SetInitialPuzzlePiecesCount()
    {
        _puzzlePiecesText.SetText("Puzzle Pieces Collected: 0/" + GameManager.Instance._puzzlePieces.Count);
    }

    public void UpdatePuzzlePiecesCollected()
    {
        _puzzlePiecesCollected++;
        _puzzlePiecesText.SetText("Puzzle Pieces Collected: " + _puzzlePiecesCollected + "/" + GameManager.Instance._puzzlePieces.Count);
    }
}
