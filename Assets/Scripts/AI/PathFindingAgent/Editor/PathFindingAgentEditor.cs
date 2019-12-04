using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathfindingAgent))]
public class PathFindingAgentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathfindingAgent pathFindingAgent = (PathfindingAgent)target;

        if (GUILayout.Button("Go to Target"))
        {
            pathFindingAgent.GoToTarget();
        }
    }
}