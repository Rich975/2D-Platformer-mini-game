using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPosX;
    public GameObject cam; // Reference to the camera
    public float parallaxFactor; // Controls the speed of the parallax effect

    private void Start()
    {
        startPosX = transform.position.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        //float temp = (cam.transform.position.x * (1 - parallaxFactor));
        float dist = (cam.transform.position.x * parallaxFactor);

        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);
    }
}