using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneConfigData))]
public class SceneConfigDataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        SceneConfigData myTarget = (SceneConfigData)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Init Slots and Length!"))
        {
            myTarget.InitDataStructure();
        }

    }
}
