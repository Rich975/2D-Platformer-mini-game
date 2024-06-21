using Cinemachine;
using UnityEngine;

public class CameraBlender : MonoBehaviour
{
    public CinemachineVirtualCamera zoomedInCam;
    public CinemachineVirtualCamera zoomedOutCam;
    public GameObject player;

    public float speedThreshold = 0.1f;
    public float blendTime = 1.0f;

    private CinemachineBrain cinemachineBrain;

    private void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        if (cinemachineBrain == null)
        {
            Debug.LogError("Cinemachine Brain not found on the main camera.");
        }
    }

    private void Update()
    {
        float speed = PlayerBehaviour.Instance.movement.x;
        Rigidbody2D playerRb = PlayerBehaviour.Instance.GetComponent<Rigidbody2D>();

        if (Mathf.Abs(speed) > speedThreshold || playerRb.velocity.y > speedThreshold)
        {
            // Blend to zoomed out camera
            zoomedOutCam.Priority = 10;
            zoomedInCam.Priority = 5;
        }
        else
        {
            // Blend to zoomed in camera
            zoomedOutCam.Priority = 5;
            zoomedInCam.Priority = 10;
        }
    }
}