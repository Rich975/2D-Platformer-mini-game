using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the platform
    public float waitTime = 1.0f; // Time to wait at each end


    public Transform platformStart;
    public Transform platformEnd;

    private bool movingToEnd = true; // Direction of platform movement
    private float timer; // Timer to manage wait time at endpoints

    // Start is called before the first frame update
    void Start()
    {
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMovement();

    }


    private void PlatformMovement()
    {
        if (timer > 0)
        {
            // Decrease timer if platform is waiting
            timer -= Time.deltaTime;
            return;
        }

        // Calculate the direction to move in
        Vector3 targetPosition = movingToEnd ? platformEnd.position : platformStart.position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the platform
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            // Toggle the direction
            movingToEnd = !movingToEnd;
            // Reset the timer
            timer = waitTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
