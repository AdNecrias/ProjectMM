using UnityEngine;

namespace MainGame {
    public class Enemy : MonoBehaviour {

        public float HP;

        public float mainAttackCooldown;

        public void Update() {
            if (HP <= 0) { Death(); }
            if (mainAttackCooldown >= 0) mainAttackCooldown -= Time.deltaTime;
        }

        public virtual void ReceiveDamage(float dmg) { }
        public virtual void Death() { }

        public bool CanAttackDueCooldown() { return mainAttackCooldown <= 0; }
    }
}
