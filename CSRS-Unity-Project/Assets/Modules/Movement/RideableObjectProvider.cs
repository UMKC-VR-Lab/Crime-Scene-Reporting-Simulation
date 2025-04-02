using UnityEngine;

/// <summary>
/// Moves the player relative to the motion (position and rotation) of a rideable object,
/// such as a moving platform, vehicle, or bucket truck arm.
/// Only applies when the player is within the rideable area (trigger zone).
/// </summary>
public class RideableObjectProvider : MonoBehaviour
{
    [Tooltip("The moving object the player should follow, e.g., the platform or bucket.")]
    public Transform rideableTransform;

    [Tooltip("The root transform of the player rig (e.g., XR Rig).")]
    public Transform playerRig;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private bool playerInside = false;

    void Start()
    {
        if (rideableTransform != null)
        {
            lastPosition = rideableTransform.position;
            lastRotation = rideableTransform.rotation;
        }
    }

    void Update()
    {
        if (!playerInside || rideableTransform == null) return;

        // --- POSITION DELTA ---
        Vector3 positionDelta = rideableTransform.position - lastPosition;
        playerRig.position += positionDelta;

        // --- ROTATION DELTA ---
        Quaternion rotationDelta = rideableTransform.rotation * Quaternion.Inverse(lastRotation);
        playerRig.rotation = rotationDelta * playerRig.rotation;

        // Store current state for next frame
        lastPosition = rideableTransform.position;
        lastRotation = rideableTransform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            lastPosition = rideableTransform.position;
            lastRotation = rideableTransform.rotation;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
