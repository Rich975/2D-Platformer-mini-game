using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    private Camera mainCamera;
    private Vector3 bulletDirection;
    [SerializeField] private float timeTillDestroy = 1f;
    private Rigidbody2D rb;
    [SerializeField] private ParticleSystem bloodSpatters_PS;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're in 2D
        bulletDirection = (mousePosition - PlayerBehaviour.Instance.gunNozzle.transform.position).normalized;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(bulletDirection * speed * Time.deltaTime);
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject, timeTillDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Instantiate(bloodSpatters_PS, contactPoint.otherCollider.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}