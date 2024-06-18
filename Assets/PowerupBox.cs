using UnityEngine;

public class PowerupBox : MonoBehaviour
{
    public enum PowerupType
    { HandGun, MachineGun, Speed }

    public PowerupType type;

    [SerializeField] private Transform powerUpSpawnPoint;

    public GameObject[] powerUps;

    [SerializeField] private float launchSpeed = 3f;

    private bool hasSpawned;

    private GameObject go;

    private Animator anim;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ParticleSystem crateExplosion;

    private int bulletHitCount = 2;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        hasSpawned = false;
        RandomPowerUp();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasSpawned)
        {
            PowerUpSpawning(collision);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletHitCount--;

            Debug.Log("bullet hit");
            Instantiate(ps, transform.position, Quaternion.identity);
            if (bulletHitCount <= 0)
            {
                PowerUpSpawnAfterBulletHit(collision);
                Instantiate(crateExplosion, transform.position, Quaternion.identity);   
                Destroy(this.gameObject);
            }
        }
    }

    private void PowerUpSpawning(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.type == PowerupType.HandGun)
        {
            go = Instantiate(powerUps[0], powerUpSpawnPoint.position, Quaternion.identity);
            hasSpawned = true;
            Destroy(this.gameObject, 1f);
        }

        if (collision.gameObject.CompareTag("Player") && this.type == PowerupType.MachineGun)
        {
            go = Instantiate(powerUps[1], powerUpSpawnPoint.position, Quaternion.identity);
            hasSpawned = true;

            Destroy(this.gameObject, 1f);
        }

        if (collision.gameObject.CompareTag("Player") && this.type == PowerupType.Speed)
        {
            go = Instantiate(powerUps[2], powerUpSpawnPoint.position, Quaternion.identity);
            hasSpawned = true;
            Destroy(this.gameObject, 1f);
        }
    }

    private void PowerUpSpawnAfterBulletHit(Collision2D collision)
    {
        if (this.type == PowerupType.HandGun)
        {
            go = Instantiate(powerUps[0], powerUpSpawnPoint.position, Quaternion.identity);
            hasSpawned = true;
            Destroy(this.gameObject, 1f);
        }

        if (this.type == PowerupType.MachineGun)
        {
            go = Instantiate(powerUps[0], powerUpSpawnPoint.position, Quaternion.identity);
            hasSpawned = true;
            Destroy(this.gameObject, 1f);
        }

        if (this.type == PowerupType.Speed)
        {
            go = Instantiate(powerUps[0], powerUpSpawnPoint.position, Quaternion.identity);
            hasSpawned = true;
            Destroy(this.gameObject, 1f);
        }
    }

    private void RandomPowerUp()
    {
        int randomPowerup = Random.Range(0, 3);
        Debug.Log(randomPowerup);

        switch (randomPowerup)
        {
            case 0:
                type = PowerupType.HandGun;
                this.GetComponentInChildren<SpriteRenderer>().material.color = Color.white;
                this.gameObject.tag = "handGunPU";

                break;

            case 1:
                type = PowerupType.MachineGun;
                this.GetComponentInChildren<SpriteRenderer>().material.color = Color.gray;
                this.gameObject.tag = "machineGunPU";

                break;

            case 2:
                type = PowerupType.Speed;
                this.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                this.gameObject.tag = "speedPU";

                break;
        }
    }
}