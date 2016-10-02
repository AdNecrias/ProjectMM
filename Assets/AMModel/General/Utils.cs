using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class Utils : MonoBehaviour {
        public static Utils instance;

        public static string PlayerTag = "player";
        public static string EnemyTag = "enemy";
        public static string ItemTag = "item";

        public static bool isEnemy(string val) { return val == EnemyTag; }
        public static bool isPlayer(string val) { return val == PlayerTag; }
        public static bool isItem(string val) { return val == ItemTag; }

        public void singleton() {
            if (instance != null) {
                Destroy(this);
                return;
            }
            instance = this;
        }

        void Awake() {
            singleton();
        }
    }
}