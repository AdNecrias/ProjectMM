using System.Collections;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class HarmAvoidanceScale : UpdateVisualsTemplate {

        [SerializeField]
        [Tooltip("Values displayed from Highest Threat to the Lowest Threat")]
        private ThreatLevel defaultThreatLevel;

        [SerializeField]
        [Tooltip("[0.1f - 10.0f] The speed which the entity is scaled.")]
        [Range(0.1f, 10.0f)]
        private float animationSpeed = 1.0f;

        //Step to be aplied each cycle
        private float step;

        [SerializeField]
        [Tooltip("Maximum size multiplier.")]
        [Range(0.0f, 10.0f)]
        private float maximumSize = 2.0f;

        [SerializeField]
        [Tooltip("Step to be aplyed when hit by a severe strike.")]
        [Range(0.0f, 10.0f)]
        private float severeStep = 1.5f;

        [SerializeField]
        [Tooltip("Step to be aplyed when hit by a high strike.")]
        [Range(0.0f, 10.0f)]
        private float highStep = 1.0f;

        [SerializeField]
        [Tooltip("Step to be aplyed when hit by a elevated strike.")]
        [Range(0.0f, 10.0f)]
        private float elevatedStep = 0.5f;

        [SerializeField]
        [Tooltip("Step to be aplyed when hit by a guarded strike.")]
        [Range(0.0f, 10.0f)]
        private float guardedStep = 0.25f;

        [SerializeField]
        [Tooltip("Step to be aplyed when hit by a low strike.")]
        [Range(-10.0f, 0.0f)]
        private float lowStep = 0.0f;

        void Awake() {
        }

        public override void UpdateVisual(EntityType type, ThreatLevel threatLevel) {
            StartCoroutine(ProgressiveScale(threatLevel));
        }

        IEnumerator ProgressiveScale(ThreatLevel threatLevel) {
            switch (threatLevel) {
                case ThreatLevel.severe: step = severeStep; break;
                case ThreatLevel.high: step = highStep; break;
                case ThreatLevel.elevated: step = elevatedStep; break;
                case ThreatLevel.guarded: step = guardedStep; break;
                case ThreatLevel.low: step = lowStep; break;
                default: Debug.LogError("Unknown Threat Level."); break;
            }
            //Check if it steps over maximum size, if so, locks at that size
            var targetSize = new Vector3(transform.localScale.x + step, transform.localScale.y + step, transform.localScale.z + step);
            if (targetSize.x >= maximumSize) targetSize = new Vector3(maximumSize, maximumSize, maximumSize);

            //We can compare any of the coordinates
            while (transform.localScale.x <= targetSize.x) {
                transform.localScale = Vector3.Lerp(transform.localScale, targetSize, Time.deltaTime * animationSpeed);
                yield return null;
            }
            yield return null;
        }
    }
}
