using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {
    public static Utils instance;

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

    public bool isPlayer(GameObject go) {
        return go.tag == "player";
    }

    public bool isEnemy(GameObject go) {
        return go.tag == "enemy";
    }

}
