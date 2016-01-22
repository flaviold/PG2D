using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathFinderGenerator))]
public class PathFinderBuildEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathFinderGenerator pGen = (PathFinderGenerator)target;
        if (GUILayout.Button("Teste"))
        {
            pGen.BuildPoints();
        }
    }
}
