using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerBehaviour : MonoBehaviour
{
    private float hInput, vInput;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpMultiplier = 5f;
    private Rigidbody2D rb;

    private SpriteRenderer sr;
    private Material originalMaterial;
    public Material whiteFlashMaterial;
    public float flashDuration = 0.1f;
    public int flashCount = 3;


    private bool isGrounded;
    private bool isFlipped;

    public Vector2 movement;
    Animator playerAnimator;
    [SerializeField] private AnimationClip playerIdleClip;


    public static PlayerBehaviour Instance;
    [SerializeField] private float intensity = 10f;

    BoxCollider2D boxCollider;

    bool isFlashing;

    

    private void Awake()
    {

        Instance = this;
        isFlipped = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();  
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        isFlashing = false; 
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        

        FlipPlayerSprite();
        PlayerAnimations();

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {

            PlayerJump();
            isGrounded = false;
        }
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
        PlayerMovement();
    }


    public void PlayerFlashing()
    {
        playerAnimator.Play("playerFlashingV2");
    }

    public void StopPlayerFlashing()
    {
        isFlashing=false;
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


    public void TurnOffLayerCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    public void TurnOnnLayerCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("touching ground");
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !isFlashing)
        {
            //get bumped
            rb.AddForce(Vector2.up * intensity, ForceMode2D.Impulse);
            isFlashing = true;  
            
            //flash white a couple of times
            PlayerFlashing();
            //TakeDamage();   

        }
        
    }
}