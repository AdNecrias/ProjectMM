using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class HarmAvoidanceTint : UpdateVisualsTemplate {

        [SerializeField]
        [Tooltip("Default threat value for this entity.")]
        [Range(0, 1)]
        private int Threat;

        public override void UpdateVisual(EntityType type, ThreatLevel threatLevel) {
            Debug.Log("Updateing Tint!!!");
        }
    }
}
