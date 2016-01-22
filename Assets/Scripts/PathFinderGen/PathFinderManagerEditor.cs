using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathFinderManager))]
public class PathFinderManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathFinderManager pMan = (PathFinderManager)target;
        if (GUILayout.Button("PegarPontos"))
        {
            pMan.BuildPoints();
        }
    }
}
