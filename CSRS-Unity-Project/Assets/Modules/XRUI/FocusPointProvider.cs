using System.Collections;
using UnityEngine;

public class FocusPointProvider : MonoBehaviour
{
    public float updatesPerSecond = 30f;
    public Transform headTransform;
    public Vector3 positionOffsetFromHead;
    public bool tiltAlongAxisZ = true;
    public bool tiltAlongAxisX = true;
    public bool trackHead = true;
    public bool smoothUpdates = true;
    public float smoothingSpeed = 5f;
    private float updateInterval;

    public Vector3 FocusPosition { get; private set; }
    public Quaternion FocusRotation { get; private set; }
    private Coroutine updateRoutine;
    private bool updateTransform = true;

    private void OnEnable()
    {
        if(updateTransform)
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
        updateTransform = true;
        updateRoutine = StartCoroutine(UpdateFocusPoint());
        updateInterval = 1f / updatesPerSecond;
    }

    public void StopUpdating()
    {
        if(updateTransform)
        {
            updateTransform = false;
            if(updateRoutine != null)
                StopCoroutine(updateRoutine);
        }
    }

    private IEnumerator UpdateFocusPoint()
    {
        if (headTransform == null)
        {
            Debug.LogError("Head Transform is null on " + gameObject.name);
            trackHead = false;
            yield break;
        }

        // Calculate target focus position
        Vector3 headFlatY = headTransform.forward;
        headFlatY.y = 0;
        headFlatY = headFlatY.normalized;
        Vector3 targetPosition = headTransform.position + (headFlatY * positionOffsetFromHead.z) + (headTransform.right * positionOffsetFromHead.x);
        targetPosition += Vector3.up * (positionOffsetFromHead.y - targetPosition.y);

        // Calculate target focus rotation
        Vector3 eulerAngles = Vector3.zero;
        eulerAngles.y = headTransform.eulerAngles.y;
        if (tiltAlongAxisZ) eulerAngles.z = headTransform.eulerAngles.z;
        if (tiltAlongAxisX) eulerAngles.x = headTransform.eulerAngles.x;
        Quaternion targetRotation = Quaternion.Euler(eulerAngles);

        // Apply smoothing if enabled
        if (smoothUpdates)
        {
            FocusPosition = Vector3.Lerp(FocusPosition, targetPosition, smoothingSpeed * Time.deltaTime);
            FocusRotation = Quaternion.Lerp(FocusRotation, targetRotation, smoothingSpeed * Time.deltaTime);
        }
        else
        {
            FocusPosition = targetPosition;
            FocusRotation = targetRotation;
        }

        // Update transform
        transform.position = FocusPosition;
        transform.rotation = FocusRotation;

        yield return new WaitForSeconds(updateInterval);

        if(updateTransform)
        {
            StartCoroutine(UpdateFocusPoint());
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the focus point in the Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(FocusPosition, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(FocusPosition, FocusPosition + FocusRotation * Vector3.forward * 0.5f);

        // Draw a line perpendicular to the forward direction to visualize rotation more thoroughly
        Gizmos.color = Color.red;
        Gizmos.DrawLine(FocusPosition, FocusPosition + FocusRotation * Vector3.right * 0.5f);
    }
}
