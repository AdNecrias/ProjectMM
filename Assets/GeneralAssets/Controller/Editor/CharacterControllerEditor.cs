using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CharacterController))]
public class CharacterControllerEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    }
}
