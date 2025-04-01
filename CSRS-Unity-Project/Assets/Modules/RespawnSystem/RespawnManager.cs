using UnityEngine;

/// <summary>
/// Allows the GameObject to be reset when entering a boundary tagged "RespawnBoundary".
/// </summary>
public class RespawnManager : MonoBehaviour
{
    [Tooltip("Reference to a TransformGroup that contains possible spawn points.")]
    public TransformGroup transformGroup;
    public Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Automatically called when this GameObject enters a trigger collider.
    /// If the other collider is tagged 'RespawnBoundary', this object will reset.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnBoundary"))
        {
            Reset();
        }
    }

    /// <summary>
    /// Resets the GameObject to the position and rotation of the given Transform.
    /// Also resets Rigidbody velocity if one is attached.
    /// </summary>
    public void Reset(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Resets the GameObject to a random spawn point from the assigned TransformGroup.
    /// </summary>
    public void Reset()
    {
        if (transformGroup != null)
        {
            Reset(transformGroup.GetRandomSpawnPoint());
        }
    }
}
