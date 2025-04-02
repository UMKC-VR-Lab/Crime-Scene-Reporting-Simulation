using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

namespace XRTools
{
    /// <summary>
    /// Manages smooth transitions between predefined transforms.
    /// </summary>
    public class TransformController : MonoBehaviour
    {
        [Tooltip("List of transforms to move between.")]
        public List<Transform> transforms;

        [Tooltip("Number of updates per second for smooth transitions.")]
        public float updatesPerSecond = 30f;

        [Tooltip("Whether to reset to the first transform when disabled.")]
        public bool resetPositionOnDisable = true;

        [Tooltip("Event triggered when moving to the starting transform.")]
        public UnityEvent onMoveToStart;

        [Tooltip("Event triggered at the beginning of a transition.")]
        public UnityEvent onBeginTransition;

        [Tooltip("Event triggered at the end of a transition.")]
        public UnityEvent onFinishTransition;

        private Transform currentTransform;
        private float updateDuration;
        private int index = 0;

        private void Start()
        {
            updateDuration = 1.0f / updatesPerSecond;
            currentTransform = transform;
        }

        private void OnDisable()
        {
            Debug.Log($"{gameObject.name} disabling.");
            if (resetPositionOnDisable && transforms.Count > 0)
            {
                MoveToStart();
            }
        }

        /// <summary>
        /// Moves to the starting transform in the list.
        /// </summary>
        public void MoveToStart()
        {
            if (transforms.Count > 0)
            {
                Transition(transform, transforms[0], 1.0f);
                index = 0;
            }
            else
            {
                Debug.LogWarning("No transforms defined.");
            }
        }

        /// <summary>
        /// Moves to the next transform in the list.
        /// If at the last transform, loops back to the first.
        /// </summary>
        public void MoveToNextTransform()
        {
            if (transforms.Count == 0)
            {
                Debug.LogWarning("No transforms defined.");
                return;
            }

            int nextIndex = (index + 1) % transforms.Count;
            Transition(transforms[index], transforms[nextIndex], 1.0f);
            index = nextIndex;
        }

        /// <summary>
        /// Starts a transition between two transforms over a specified duration.
        /// </summary>
        /// <param name="start">The starting transform.</param>
        /// <param name="end">The target transform.</param>
        /// <param name="duration">The duration of the transition in seconds.</param>
        public void Transition(Transform start, Transform end, float duration)
        {
            if (end == transforms[0])
            {
                onMoveToStart.Invoke();
            }

            onBeginTransition.Invoke();
            StartCoroutine(LerpTransform(start, end, duration));
        }

        /// <summary>
        /// Smoothly interpolates between two transforms.
        /// </summary>
        /// <param name="start">The starting transform.</param>
        /// <param name="end">The target transform.</param>
        /// <param name="duration">The interpolation duration in seconds.</param>
        /// <returns>IEnumerator for coroutine.</returns>
        private IEnumerator LerpTransform(Transform start, Transform end, float duration)
        {
            float elapsedTime = 0f;

            Vector3 startPosition = start.position;
            Quaternion startRotation = start.rotation;
            Vector3 startScale = start.localScale;

            Vector3 endPosition = end.position;
            Quaternion endRotation = end.rotation;
            Vector3 endScale = end.localScale;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;

                // Interpolate position, rotation, and scale
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
                transform.localScale = Vector3.Lerp(startScale, endScale, t);

                elapsedTime += updateDuration;
                yield return new WaitForSeconds(updateDuration);
            }

            // Ensure the final values are set
            ApplyTransform(end);
            onFinishTransition.Invoke();
        }

        /// <summary>
        /// Applies a transform's position, rotation, and scale to this object.
        /// </summary>
        /// <param name="target">The transform to apply.</param>
        private void ApplyTransform(Transform target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
            transform.localScale = target.localScale;
        }
    }
}
