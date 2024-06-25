using UnityEngine;

public class Coin : MonoBehaviour
{
    private ParticleSystem particleSystem;
    [SerializeField] private GameObject coinSpritegameObject;
    private Animator animator;
    private bool isCollected;

    // Start is called before the first frame update
    private void Start()
    {
        isCollected = false;
        particleSystem = GetComponent<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    if (!isCollected)
        //    {
        //        UIManager.Instance.AddScore(1);
        //        isCollected = true;
        //        animator.SetBool("isCollected", true);
        //        Destroy(gameObject, 1);
        //    }
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isCollected)
            {
                UIManager.Instance.AddScore(1);
                isCollected = true;
                animator.SetBool("isCollected", true);
                Destroy(gameObject, 1);
            }
        }

    }

}