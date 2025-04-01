using UnityEngine;

/// <summary>
/// Holds a list of Transforms that can be used as spawn points for resetting objects.
/// Attach this to an empty GameObject and assign child transforms as spawn points.
/// </summary>
public class TransformGroup : MonoBehaviour
{
    [Tooltip("List of spawn points to choose from when resetting objects.")]
    public Transform[] transforms;

    /// <summary>
    /// Returns a random spawn point from the list. 
    /// Falls back to this object's transform if none are assigned.
    /// </summary>
    public Transform GetRandomSpawnPoint()
    {
        if (transforms.Length == 0) return transform;
        return transforms[Random.Range(0, transforms.Length)];
    }

    /// <summary>
    /// Returns the closest spawn point to the start. 
    /// Placeholder until students fill in.
    /// </summary>
    public Transform GetClosestSpawnPoint(Transform start) {
        return GetRandomSpawnPoint();
    }
}
