using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace XRTools
{
    /// <summary>
    /// Manages a list of GameObject groups, displaying them in sequence with optional cycling behavior.
    /// </summary>
    public class ProgressionManager : MonoBehaviour
    {
        [Tooltip("List of GameObject groups to display in sequence.")]
        public List<GameObjectGroup> gameObjects;

        [Tooltip("Event triggered when the displayed GameObject changes.")]
        public UnityEvent onChangeDisplayedGameObject;

        [Tooltip("Event triggered when the final GameObject is displayed.")]
        public UnityEvent onFinishedDisplayingFinalGameObject;

        [Tooltip("If true, the sequence loops after the last GameObject.")]
        public bool loop = false;

        [Tooltip("If true, resets progress to the first group of gameobjects when the object is enabled.")]
        public bool resetOnEnable = true;

        [Tooltip("Enables debug logging.")]
        public bool debug = false;

        private int currentIndex = -1;

        private void Start()
        {
            ResetProgression();
        }

        private void OnEnable()
        {
            if (resetOnEnable) ResetProgression();
            if(debug) Debug.Log(gameObject.name + ": Resetting on enable");
        }

        /// <summary>
        /// Resets the progression by deactivating all GameObjects and starting from the beginning.
        /// </summary>
        public void ResetProgression()
        {
            DeactivateAllGameObjects();
            currentIndex = -1;
            ShowNextGameObject();
            if(debug) Debug.Log(gameObject.name + ": Resetting Progress");
        }

        /// <summary>
        /// Deactivates all GameObjects in the list.
        /// </summary>
        public void DeactivateAllGameObjects()
        {
            foreach (var group in gameObjects)
                SetAllGameObjectsActiveState(group.gameObjects, false);
        }

        /// <summary>
        /// Displays the next GameObject group in the list.
        /// </summary>
        public void ShowNextGameObject()
        {
            ShowGameObjectByIndex(currentIndex + 1);
        }

        /// <summary>
        /// Displays the GameObject group at the given index, if valid.
        /// </summary>
        public void ShowGameObjectByIndex(int index)
        {
            if (gameObjects == null || gameObjects.Count == 0)
            {
                if (debug) Debug.LogWarning("No GameObjects in the list.");
                return;
            }

            if(loop == false && index > gameObjects.Count) 
            {
                if (debug) Debug.LogWarning($"{gameObject.name}: Attempted to access an overflow index.");
                return;
            }
            if (index < 0)
            {
                if (debug) Debug.LogWarning($"{gameObject.name}: Attempted to access a negative index.");
                return;
            }

            // Deactivate the old group if it's valid
            if (currentIndex >= 0 && currentIndex < gameObjects.Count)
            {
                SetAllGameObjectsActiveState(gameObjects[currentIndex].gameObjects, false);
            }

            currentIndex = index;

            // Check if we've gone past the end
            if (currentIndex >= gameObjects.Count)
            {
                if (loop)
                {
                    ResetProgression();
                    // After resetting, currentIndex gets set to -1, so set it back:
                    currentIndex = 0; 
                    SetAllGameObjectsActiveState(gameObjects[currentIndex].gameObjects, true);
                    onChangeDisplayedGameObject.Invoke();
                    if (debug) Debug.Log($"{gameObject.name}: looping");
                }
                else
                {
                    if (debug) Debug.Log($"{gameObject.name}: Reached the final GameObject.");
                }
                onFinishedDisplayingFinalGameObject.Invoke();
                return;
            }

            // Activate the new group if it's within range
            SetAllGameObjectsActiveState(gameObjects[currentIndex].gameObjects, true);
            onChangeDisplayedGameObject.Invoke();

            if (debug) Debug.Log($"Showing GameObject group at index {currentIndex}");
        }

        /// <summary>
        /// Sets all GameObjects in the given list to the specified active state.
        /// </summary>
        private void SetAllGameObjectsActiveState(List<GameObject> list, bool setToActive)
        {
            foreach (var obj in list)
            {
                obj.SetActive(setToActive);
            }
        }
    }

    /// <summary>
    /// Represents a group of GameObjects managed by the ProgressionManager.
    /// </summary>
    [System.Serializable]
    public class GameObjectGroup
    {
        [Tooltip("List of GameObjects in this group.")]
        public List<GameObject> gameObjects;
    }
}
