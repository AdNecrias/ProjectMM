using UnityEngine;
using UnityEditor;

namespace AdNecriasMeldowMethod {
    [CustomEditor(typeof(AMMPlayer))]
    public class AMMPlayerEditor : Editor {

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            AMMPlayer script = (AMMPlayer)target;

            //NSList Editor
            var dic = script.NSList;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Total enemies encountered: " + script.GetTotalEnemiesEncountered());
            GUILayout.EndHorizontal();


            foreach (var d in dic) {
                GUILayout.Label("AMMEnemy: " + d.Key, EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Rarity: " + d.Value.R);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Total encounters: " + d.Value.T);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Rarity: " + d.Value.Rarity);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
            }
        }
    }
}
