using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CycleList))]
public class CycleListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CycleList cycleList = (CycleList)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Toggle Mode"))
        {
            cycleList.AddNumber();
            Debug.Log("Added another number");
        }
        if (GUILayout.Button("Start Cycling"))
        {
            cycleList.StartCycling();
        }

        if (GUILayout.Button("Stop Cycling"))
        {
            cycleList.StopCycling();
        }

        if (GUILayout.Button("Toggle Mode"))
        {
            cycleList.ToggleMode();
            Debug.Log("Mode toggled to " + cycleList.mode);
        }

    }
}
