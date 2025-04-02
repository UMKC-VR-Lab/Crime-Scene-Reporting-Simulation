using UnityEngine.Events;
using UnityEngine;

namespace XRTools
{
    /// <summary>
    /// Detects trigger events with specified tagged objects and invokes UnityEvents accordingly.
    /// Requires a Collider component set to trigger mode.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class VolumeTrigger : MonoBehaviour
    {
        [Tooltip("The tag of objects that will trigger this event.")]
        public string triggerTag = "";

        [Tooltip("Event invoked when a tagged object enters the trigger.")]
        public UnityEvent onTriggerEnter;

        [Tooltip("Event invoked when a tagged object stays inside the trigger.")]
        public UnityEvent onTriggerStay;

        [Tooltip("Event invoked when a tagged object exits the trigger.")]
        public UnityEvent onTriggerExit;

        /// <summary>
        /// Called when another collider enters this object's trigger collider.
        /// </summary>
        /// <param name="other">The collider entering the trigger.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(triggerTag))
            {
                Debug.Log($"Object Entered: {other.tag}");
                onTriggerEnter.Invoke();
            }
        }

        /// <summary>
        /// Called once per frame while another collider remains inside this object's trigger collider.
        /// </summary>
        /// <param name="other">The collider staying in the trigger.</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(triggerTag))
            {
                onTriggerStay.Invoke();
            }
        }

        /// <summary>
        /// Called when another collider exits this object's trigger collider.
        /// </summary>
        /// <param name="other">The collider exiting the trigger.</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(triggerTag))
            {
                onTriggerExit.Invoke();
            }
        }
    }
}
