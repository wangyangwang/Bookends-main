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

        
        if (GUILayout.Button("save to json file"))
        {
            myTarget.ConvertToJson();
        }

        if (GUILayout.Button("load json file"))
        {
            myTarget.LoadJson();
        }

    }
}
