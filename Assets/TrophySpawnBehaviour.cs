using UnityEngine;

public class TrophySpawnBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    private float hMin = -0.3f, hMax = 0.3f;

    // Start is called before the first frame update
    private void Start()
    {
        Vector2 randomVector = new Vector2(Random.Range(hMin, hMax), 1);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomVector * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}