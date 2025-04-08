using UnityEngine;

public class MatchTransform : MonoBehaviour
{
    public float positionLagTime = 0.5f;
    public float rotationLagTime = 0.5f;
    public Transform source;
    public Transform target;

    private Vector3 positionVelocity;
    private float angularVelocity;
    public bool update = true;

    public void SetSource(GameObject newSource)
    {
        source = newSource.transform;
    }

    private void Update()
    {
        if(update == false) {
            return;
        }
        if (source == null || target == null)
        {
            Debug.LogWarning("Source or Target is not set for MatchTransform on " + gameObject.name);
            return;
        }

        // Smoothly update position with lag
        target.position = Vector3.SmoothDamp(
            target.position,
            source.position,
            ref positionVelocity,
            positionLagTime
        );

        // Smoothly update rotation with lag
        target.rotation = SmoothDampQuaternion(
            target.rotation,
            source.rotation,
            ref angularVelocity,
            rotationLagTime
        );
    }

    private Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref float velocity, float smoothTime)
    {
        // Ensure quaternions are normalized to avoid instability
        current = Quaternion.Normalize(current);
        target = Quaternion.Normalize(target);

        // Calculate the angular difference in degrees
        float angleDifference = Quaternion.Angle(current, target);

        // Smooth the angular velocity
        float smoothedAngle = Mathf.SmoothDampAngle(0, angleDifference, ref velocity, smoothTime);

        // Interpolate the rotation using the smoothed angle
        return Quaternion.RotateTowards(current, target, smoothedAngle);
    }
}
