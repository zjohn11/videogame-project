using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Distance between the player and camera
    public float smoothSpeed = 0.125f;  // Smoothness of the camera movement

    void Start()
    {
        // Set the initial offset if not manually set
        if (offset == Vector3.zero)
            offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Desired position is the player's position + the offset
        Vector3 desiredPosition = player.position + offset;

        // Smooth the camera's movement to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera position
        transform.position = smoothedPosition;

        // Optionally, make the camera look at the player (this can be turned off if not needed)
        transform.LookAt(player);
    }
}