using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntitySpawner))]
public class EntitySpawnerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        // if (GUILayout.Button("TODO"))
        // {
        //     GameSettings.Instance.pauseEnabled = !GameSettings.Instance.pauseEnabled;
        // }
        DrawDefaultInspector();
    }
}
