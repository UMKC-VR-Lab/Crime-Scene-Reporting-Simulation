using UnityEditor;
using UnityEngine;

namespace XRTools
{
    [CustomEditor(typeof(ProgressionManager))]
    public class ProgressionManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProgressionManager progressor = (ProgressionManager)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Show Next GameObject"))
            {
                progressor.ShowNextGameObject();
            }

            if (GUILayout.Button("Reset Progression"))
            {
                progressor.ResetProgression();
            }
        }
    }
}