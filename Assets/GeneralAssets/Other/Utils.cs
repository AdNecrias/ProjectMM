﻿using UnityEngine;

namespace MainGame {
    public class Utils : MonoBehaviour {
        public static Utils instance;

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
