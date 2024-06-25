using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 direction;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private int hitCount;

    [SerializeField] private Animator anim;

    [SerializeField] private int timesHit = 3;
    [SerializeField] private ParticleSystem bloodExplosion_PS;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        direction = new Vector2(1, 0);
        sr.flipX = false;
    }

    // Update is called once per frame
    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            ChangeDirection();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletHit();

            timesHit--;

            if (timesHit <= 0)
            {
                Instantiate(bloodExplosion_PS, transform.position, Quaternion.identity);
                CoinSpawner.Instance.SpawnCoins(CoinSpawner.Instance.RandomNumberOfCoins(), transform.position);

                Destroy(this.gameObject);
                timesHit = 3;
            }
        }
    }

    private void ChangeDirection()
    {
        if (direction.x == -1)
        {
            direction = new Vector2(1, 0);
            sr.flipX = false;
        }
        else if (direction.x == 1)
        {
            direction = new Vector2(-1, 0);
            sr.flipX = true;
        }
    }

    private void BulletHit()
    {
        anim.SetBool("isHit", true);
    }

    public void ResetHit()
    {
        anim.SetBool("isHit", false);
    }
}