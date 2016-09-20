using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MainGame.AxisAlignedCharacterController))]
public class AxisAlignedCharacterController : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    }
}
