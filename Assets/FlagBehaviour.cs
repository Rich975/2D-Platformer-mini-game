using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private GameObject trophy;
    [SerializeField] private Transform trophySpawn;

    private ParticleSystem ps;
    private bool hasTriggered;

    // Start is called before the first frame update
    private void Start()
    {
        trophySpawn = GetComponentInChildren<Transform>();
        ps = GetComponentInChildren<ParticleSystem>();
        hasTriggered = false;
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            animator.SetTrigger("levelFlagRaise");

            Debug.Log("triggering levelcomplete");
            UIManager.Instance.LevelComplete();

            SpawnTrophy();

            ps.Play();
            hasTriggered = true;
        }
    }

    private void SpawnTrophy()
    {
        Instantiate(trophy, trophySpawn.position, Quaternion.identity);
        animator.SetTrigger("trophySpawn");
    }
}