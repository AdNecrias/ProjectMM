using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class Enemy : MonoBehaviour {

        //Delegate that will contain all the updates necessary to change visual representation of the current entity
        private delegate void UpdateVisuals(EntityType type, ThreatLevel threatLevel);
        private UpdateVisuals updateVisuals;
        public EntityType Type;

        void OnEnable() { AMMPlayer.OnEntityInteracted += UpdateEntity; }
        void OnDisable() { AMMPlayer.OnEntityInteracted -= UpdateEntity; }

        void Awake() {
            foreach (var cmpt in GetComponents<UpdateVisualsTemplate>()) {
                updateVisuals += cmpt.UpdateVisual;
            }
        }

        private void UpdateEntity(EntityType type, ThreatLevel threatLevel) {
            //Check if I am supose to update using type
            if (updateVisuals != null && type == Type) updateVisuals(type, threatLevel);
        }
    }
}
