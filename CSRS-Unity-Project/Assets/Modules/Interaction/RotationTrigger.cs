using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RotationTrigger : MonoBehaviour
{
    public Transform target;
    public bool update = true;
    public int updatesPerSecond = 10;

    public UnityEvent onBecomeTrue;
    public UnityEvent onStayTrue;
    public UnityEvent onBecomeFalse;

    [Range(0f, 180f)]
    public float angleThreshold = 30f;

    private Coroutine updateRoutine;
    private float updateInterval;

    private bool wasWithinAngle;

    private void OnEnable()
    {
        if (update)
        {
            StartUpdating();
        }
    }

    private void OnDisable()
    {
        StopUpdating();
    }

    public void StartUpdating()
    {
        if (!update)
            update = true;

        updateInterval = 1f / updatesPerSecond;
        updateRoutine = StartCoroutine(CheckRotation());
    }

    public void StopUpdating()
    {
        if (update)
        {
            update = false;
            if (updateRoutine != null)
            {
                StopCoroutine(updateRoutine);
            }
        }
    }

    private IEnumerator CheckRotation()
    {
        if (target == null)
        {
            Debug.LogError("Target Transform is null on " + gameObject.name);
            yield break;
        }

        // Determine if target's up vector is within angleThreshold degrees of world up.
        bool isWithinAngle = Vector3.Angle(target.up, Vector3.up) <= angleThreshold;

        // Compare with previous frame’s state to see if it changed.
        if (isWithinAngle != wasWithinAngle)
        {
            if (isWithinAngle)
            {
                // Just entered the "true" zone
                onBecomeTrue?.Invoke();
            }
            else
            {
                // Just left the "true" zone
                onBecomeFalse?.Invoke();
            }
        }
        else
        {
            // If it didn’t change and it’s still true, fire onStayTrue
            if (isWithinAngle)
            {
                onStayTrue?.Invoke();
            }
        }

        // Store state for next iteration
        wasWithinAngle = isWithinAngle;

        // Wait before next check
        yield return new WaitForSeconds(updateInterval);

        // Recursively restart if we're still updating
        if (update)
        {
            StartCoroutine(CheckRotation());
        }
    }
}
