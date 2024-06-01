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
    private bool tempBool;

    public Vector2 movement;

    private void Awake()
    {
        isFlipped = false;
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            PlayerJump();
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
        rb.AddForce(Vector2.up * jumpMultiplier * Time.deltaTime, ForceMode2D.Impulse);
        //isGrounded = false;
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("touching ground");
            isGrounded = true;
        }
    }
}