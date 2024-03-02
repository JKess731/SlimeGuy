using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Codice.Client.BaseCommands;

[CustomEditor(typeof(SlimeSplit))]
public class SlimeSplitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SlimeSplit slimeSplit = (SlimeSplit)target;

        GUILayout.Label("--- Minion Counter ---");
        slimeSplit.minionCounter = EditorGUILayout.IntSlider(slimeSplit.minionCounter, 1, 10); ;
    }
}
