using UnityEngine;

namespace MainGame {
    public class Enemy : MonoBehaviour {

        public float HP;

        public void Update() {
            if (HP <= 0) {
                Death();
            }
        }

        public virtual void ReceiveDamage(float dmg) { }
        public virtual void Death() { }

    }
}
