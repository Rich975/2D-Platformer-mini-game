using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Renamed for clarity
    private List<GameObject> coins = new List<GameObject>();
    [SerializeField] private GameObject coinParticles;

    [SerializeField] private float minSpeed = 5f, maxSpeed = 10f;
    private float speed;


    private float hMin = -0.3f, hMax = 0.3f;

    [SerializeField] private int minCoinAmount = 1; // Added default values
    [SerializeField] private int maxCoinAmount = 10; // Added default values

    [SerializeField] private Transform spawnPosition; // Renamed for clarity

    public static CoinSpawner Instance;

    private void Awake()
    {
        Instance = this;

    }


    private void Start()
    {


        int coinCount = RandomNumberOfCoins();
        SpawnCoins(coinCount, spawnPosition.position);
    }

    public int RandomNumberOfCoins()
    {
        return Random.Range(minCoinAmount, maxCoinAmount);
    }

    public void SpawnCoins(int coinCount, Vector3 pos)
    {
        spawnPosition.position = pos;
        if (coinParticles != null)
        {
            Instantiate(coinParticles, spawnPosition.position, Quaternion.identity);
        }

        for (int i = 0; i < coinCount; i++)
        {
            GameObject coinInstance = Instantiate(coinPrefab, spawnPosition.position, Quaternion.identity);
            coins.Add(coinInstance);

            Rigidbody2D rb = coinInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                speed = Random.Range(minSpeed, maxSpeed);
                Vector2 randomVector = new Vector2(Random.Range(hMin, hMax), 1);
                rb.AddForce(randomVector * speed, ForceMode2D.Impulse);
            }
        }
    }
}
