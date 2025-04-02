using UnityEngine;
using UnityEditor;

namespace XRTools
{
    [CustomEditor(typeof(TimeTrigger))]
    public class TimeTriggerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TimeTrigger timeTrigger = (TimeTrigger)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Test On Timer Began"))
            {
                timeTrigger.onTimerBegan.Invoke();
            }
            if (GUILayout.Button("Test On Timer Cancelled"))
            {
                timeTrigger.onTimerCancelled.Invoke();
            }
            if (GUILayout.Button("Test On Timer Completed"))
            {
                timeTrigger.onTimerCompleted.Invoke();
            }
        }
    }
}
