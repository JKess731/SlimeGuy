using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RoomTriggerControl))]
public class RoomTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomTriggerControl trigger = (RoomTriggerControl)target;

        GUILayout.Label("Room Danger Level");
        trigger.dangerLevel = EditorGUILayout.IntSlider(trigger.dangerLevel, 2, 24);

        if (GUILayout.Button("Spawn New Enemies"))
        {
            // Make this so that enemies can only be spawned AFTER the initial spawn.
            trigger.SpawnEnemies(trigger.spawners);

            Debug.Log("Button pressed");
        }
    }

}
