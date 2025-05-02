using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeleprompterManager))]
public class TeleprompterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TeleprompterManager manager = (TeleprompterManager)target;

        GUILayout.Space(10);
        GUILayout.Label("Teleprompter Controls", EditorStyles.boldLabel);

        if (GUILayout.Button(manager.IsScrolling() ? "Pause Autoscroll" : "Start Autoscroll"))
        {
            manager.ToggleAutoscroll();
        }

        if (GUILayout.Button("Reset Scroll Position"))
        {
            manager.ResetContentPosition();
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Increase Scroll Speed"))
        {
            manager.IncreaseScrollSpeed();
        }
        if (GUILayout.Button("Decrease Scroll Speed"))
        {
            manager.DecreaseScrollSpeed();
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Reset Scroll Speed"))
        {
            manager.ResetScrollSpeed();
        }
        if (GUILayout.Button("Back a Few Lines"))
        {
            manager.BackALine();
        }

        // Ensure changes are saved and reflected in the editor
        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
        }
    }
}
