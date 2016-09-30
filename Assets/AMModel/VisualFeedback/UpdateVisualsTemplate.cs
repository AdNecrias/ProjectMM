using UnityEngine;

namespace AdNecriasMeldowMethod {
    [RequireComponent(typeof(Enemy))]
    public class UpdateVisualsTemplate : MonoBehaviour {
        public virtual void UpdateVisual(EntityType type, ThreatLevel threatLevel) { }
    }
}
