using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float hInput, vInput;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpMultiplier = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGrounded;
    private bool isFlipped;

    public Vector2 movement;
    Animator playerAnimator;
    [SerializeField] private AnimationClip playerIdleClip;


    public static PlayerBehaviour Instance;


    private void Awake()
    {
        Instance = this;
        isFlipped = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();  
    }

    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMovement();

        FlipPlayerSprite();
        PlayerAnimations();

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {

            PlayerJump();
            isGrounded = false;
        }
    }

    private void PlayerMovement()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        movement = new Vector2(hInput, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void PlayerJump()
    {  
        rb.AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);
    }


    public void PlayerAnimations()
    {
        if(movement.x != 0)
        {
            playerAnimator.SetBool("isMoving", true);
        } else
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("touching ground");
            isGrounded = true;

        }

        
    }
}