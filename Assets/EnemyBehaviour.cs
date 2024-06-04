using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 direction;
    [SerializeField] private SpriteRenderer sr;


    // Start is called before the first frame update
    private void Start()
    {
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("colliding with obstacle");
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


    }
}