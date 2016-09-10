using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ControllerManager))]
public class ControllerManagerEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        /* //Not working as intended
        AMControllerManager manager = (AMControllerManager)target;
        if (GUILayout.Button("Reset Singleton")) {
            manager.singleton();
        }*/
    }
}
