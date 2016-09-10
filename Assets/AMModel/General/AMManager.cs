using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMManager : MonoBehaviour {
        public AMMPlayer _AMMPlayer;
        [Tooltip("Reference to AMMPlayer component.")]
        [SerializeField]
        public static AMMPlayer AMMPlayer;

        public Material _NsMaterial;
        [Tooltip("Reference to Novelty Seeking positive values material to be applied on elements.")]
        [SerializeField]
        public static Material NsMaterial;

        void Awake() {
            AMMPlayer = _AMMPlayer;
            NsMaterial = _NsMaterial;
        }
    }

}