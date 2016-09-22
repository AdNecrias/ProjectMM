using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMMUtils : MonoBehaviour {
        public static AMMUtils instance;

        public string PlayerTag;
        public string EnemyTag;

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