using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float hInput, vInput;

    public float normalSpeed = 10f;
    public float powerupSpeed = 20f;
    public float speedPowerupDuration = 5f;

    private float speedPowerupTimer = 0f;
    private bool hasSpeedPowerUp = false;

    [SerializeField] private float jumpMultiplier = 5f;
    private Rigidbody2D rb;

    private SpriteRenderer sr;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    private bool isGrounded;
    private bool isFlipped;
    private bool isMoving;

    public Vector2 movement;
    private Animator playerAnimator;
    [SerializeField] private AnimationClip playerIdleClip;

    public static PlayerBehaviour Instance;
    [SerializeField] private float intensity = 10f;

    private BoxCollider2D boxCollider;

    private bool isFlashing;
    public int score;

    public Transform weaponTransform;
    public Transform weaponRotateOrigin;
    public Transform gunNozzle;

    public Transform tempTransform;

    public GameObject nozzleFlash;
    public GameObject bullet;
    public GameObject handGunPU;
    public GameObject machineGunPU;
    public GameObject speedPU;

    public Camera mainCamera;

    public SpriteRenderer handGunSpriteRenderer;

    private bool hasGunPowerUp;

    private float angle;
    private PowerupBox powerupBox;

    private float playerHealth = 100f;

    private void Awake()
    {
        tempTransform = weaponTransform;
        Instance = this;
        isFlipped = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        score = 0;
        isFlashing = false;
        isMoving = false;
        sr = GetComponentInChildren<SpriteRenderer>();
        hasGunPowerUp = false;
        hasSpeedPowerUp = false;    
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasGunPowerUp)
        {
            SpawnBullet();
            //fire a projectile from the gun nozzle
        }


        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            PlayerJump();
            isGrounded = false;
        }

        if (isGrounded)
        {
            playerAnimator.SetBool("isJumping", false);
        }

        FlipPlayerSprite();
        PlayerAnimations();
        IncreasePlayerSpeed();
      
        
    }

    private void AimWeapon()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're in 2D

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (mousePosition - transform.position).normalized;

        Debug.DrawLine(transform.position, mousePosition, Color.red);

        // Calculate the angle in degrees
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the weapon to face the mouse pointer
        weaponRotateOrigin.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void SpawnBullet()
    {
        Instantiate(bullet, gunNozzle.position, Quaternion.identity);

        Instantiate(nozzleFlash, gunNozzle.position, Quaternion.identity);
    }

    public void TurnOffBoxCollider2D()
    {
        boxCollider.enabled = false;
    }

    public void TurnOnBoxCollider2D()
    {
        boxCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        AimWeapon();
        PlayerMovement();
    }

    public void PlayerFlashing()
    {
        playerAnimator.Play("playerFlashing");
    }

    public void StopPlayerFlashing()
    {
        isFlashing = false;
    }

    private void PlayerMovement()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        movement = new Vector2(hInput, 0) * normalSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void PlayerJump()
    {
        rb.AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);
    }

    public void PlayerAnimations()
    {
        if (movement.x != 0)
        {
            isMoving = true;
            playerAnimator.SetBool("isMoving", true);
        }
        else
        {
            isMoving = false;
            playerAnimator.SetBool("isMoving", false);
        }
    }

    private void FlipPlayerSprite()
    {
        if (movement.x < 0)
        {
            isFlipped = true;
            sr.flipX = true;
        }
        else if (movement.x > 0)
        {
            isFlipped = false;
            sr.flipX = false;
        }
    }

    public void TurnOffLayerCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    public void TurnOnLayerCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void TakeDamage()
    {
        Debug.Log("Player took damage!");  //TODO: add damage code
    }


    public void ActivateSpeedPowerUp()
    {
        hasSpeedPowerUp = true;
        speedPowerupTimer = speedPowerupDuration;
    }


    public void IncreasePlayerSpeed()
    {

        if (hasSpeedPowerUp)
        {
            if (speedPowerupTimer > 0)
            {
                speedPowerupTimer -= Time.deltaTime;
                Debug.Log("Power-up active. Time remaining: " + speedPowerupTimer);
                normalSpeed = powerupSpeed;
            }
            else
            {
                hasSpeedPowerUp = false;
                normalSpeed = 10f;
                Debug.Log("Power-up expired. Speed reset to normal.");
            }
        }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        powerupBox = collision.gameObject.GetComponent<PowerupBox>();
        if (powerupBox != null)
        {
            Debug.Log(powerupBox);
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !isFlashing)
        {
            //get bumped
            rb.AddForce(Vector2.up * intensity, ForceMode2D.Impulse);
            isFlashing = true;

            //flash transparant a couple of times
            PlayerFlashing();
            TakeDamage();
        }

        if (collision.gameObject.CompareTag("handGunPU") || collision.gameObject.CompareTag("machineGunPU"))
        {
            AttachWeaponV2();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("speedPU"))
        {
            ActivateSpeedPowerUp();
            Destroy(other.gameObject);
        }

    }


    private void AttachWeaponV2()
    {
        if (!hasGunPowerUp)
        {
            if (powerupBox.CompareTag("handGunPU"))
            {
                GameObject playerGun = Instantiate(handGunPU, weaponTransform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
                playerGun.transform.SetParent(weaponRotateOrigin, true);
                gunNozzle = playerGun.GetComponentInChildren<Transform>().Find("gunNozzle");
                hasGunPowerUp = true;
            }

            if (powerupBox.CompareTag("machineGunPU"))
            {
                GameObject playerGun = Instantiate(machineGunPU, weaponTransform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
                playerGun.transform.SetParent(weaponRotateOrigin, true);
                gunNozzle = playerGun.GetComponentInChildren<Transform>().Find("gunNozzle");
                hasGunPowerUp = true;
            }

            return;
        }
    }
}