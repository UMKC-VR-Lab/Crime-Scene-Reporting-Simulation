using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;   // for IEnumerator
using System.IO;           // for Path functions

/// <summary>
/// SceneHandler is a MonoBehaviour that manages asynchronous scene loading.
/// Attach this script to a GameObject to use its scene loading functionality.
/// </summary>
public class SceneHandler : MonoBehaviour
{
    /// <summary>Event fired to report scene loading progress (0.0 to 1.0).</summary>
    public event Action<float> OnLoadProgress;
    
    /// <summary>Event fired when the new scene has finished loading.</summary>
    public event Action OnLoadComplete;

    /// <summary>
    /// Begins loading a new scene asynchronously by its name.
    /// The current scene will be unloaded once the new scene is loaded (Single mode).
    /// </summary>
    /// <param name="sceneName">Name of the scene to load (must be added in Build Settings).</param>
    public void LoadSceneAsyncByName(string sceneName)
    {
        // Start the coroutine to load the scene by name.
        StartCoroutine(LoadSceneAsyncRoutine(sceneName, -1));
    }

    /// <summary>
    /// Begins loading a new scene asynchronously by its build index.
    /// The current scene will be unloaded once the new scene is loaded (Single mode).
    /// </summary>
    /// <param name="sceneBuildIndex">Build index of the scene to load (as per Build Settings order).</param>
    public void LoadSceneAsyncByIndex(int sceneBuildIndex)
    {
        // Start the coroutine to load the scene by index.
        StartCoroutine(LoadSceneAsyncRoutine(null, sceneBuildIndex));
    }

    /// <summary>
    /// Coroutine that performs the asynchronous scene loading and unloading.
    /// Always uses LoadSceneMode.Single to replace the current scene&#8203;:contentReference[oaicite:4]{index=4}.
    /// Provides progress updates and invokes completion events.
    /// </summary>
    private IEnumerator LoadSceneAsyncRoutine(string sceneName, int sceneIndex)
    {
        // Begin to load the scene asynchronously in Single mode (non-additive).
        AsyncOperation loadOperation;
        if (!string.IsNullOrEmpty(sceneName))
        {
            loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        else
        {
            loadOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }

        // If the scene name or index is invalid, LoadSceneAsync will throw an error (scene must be in Build Settings).
        // Progress is reported between 0.0 and 0.9 while the scene is loading, then 1.0 when done&#8203;:contentReference[oaicite:5]{index=5}.
        while (!loadOperation.isDone)
        {
            // Invoke progress event with current progress (clamped 0.0 to 0.9 before completion).
            OnLoadProgress?.Invoke(loadOperation.progress);
            yield return null;  // wait for next frame and continue looping
        }

        // The scene is fully loaded at this point (isDone = true).
        // Unity will have unloaded the previous scene automatically in Single mode&#8203;:contentReference[oaicite:6]{index=6}.
        // Invoke one last progress update of 100% (progress becomes 1.0 when done).
        OnLoadProgress?.Invoke(1f);
        // Invoke the completion event to signal that the new scene is ready.
        OnLoadComplete?.Invoke();
    }

    /// <summary>
    /// Retrieves the names of all scenes included in the Build Settings.
    /// </summary>
    /// <returns>An array of scene names as strings.</returns>
    public string[] GetAllScenesInBuild()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;  // total number of scenes in Build Settings&#8203;:contentReference[oaicite:7]{index=7}
        string[] sceneNames = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            // Get the scene path by build index (e.g., "Assets/Scenes/MyScene.unity")&#8203;:contentReference[oaicite:8]{index=8}
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            // Extract just the scene name without extension from the path.
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            sceneNames[i] = sceneName;
        }
        return sceneNames;
    }
}
