using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformGroup))]
public class TransformGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TransformGroup group = (TransformGroup)target;

        if (GUILayout.Button("Add All Children As transforms"))
        {
            Undo.RecordObject(group, "Add Children to transforms");

            int childCount = group.transform.childCount;
            Transform[] newPoints = new Transform[childCount];

            for (int i = 0; i < childCount; i++)
            {
                newPoints[i] = group.transform.GetChild(i);
            }

            group.transforms = newPoints;
            EditorUtility.SetDirty(group);
        }
    }
}
