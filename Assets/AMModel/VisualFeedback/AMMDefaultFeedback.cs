using System;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    /*
     * Novelty Seeking Visual Feedback
     * This class provides default visual feedback on objects according to the Novelty Seeking parameter
     */
    public class AMMDefaultFeedback : AMMEnemy {

        public Color color = Color.cyan;

        protected override void UpdateVisual(AMMEnemyCategory ec) {
            UpdateNoveltySeeking(ec);

            //StartCoroutine(UpdateVisualTransistionCR(R, 0.1f));
        }

        private void UpdateNoveltySeeking(AMMEnemyCategory ec) {
            foreach (var renderer in RendererLstList) {
                //version 0.1
                foreach (var material in renderer.materials) {
                    try {
                        //var color = material.GetColor("_AM_NSOutline");

                        var localColor = Color.Lerp(color, Color.black, (1 - ec.R) * 0.5f + 0.5f);

                        material.SetColor("_AM_NSOutline", localColor);

                        //color.a = Mathf.Clamp01(ec.R);
                        //material.SetColor("_AM_NSOutline", color);
                    } catch (Exception e) {
                        //TODO :: make this show only once.
                        Debug.Log("-- " + transform.name + "OutlineColor not found. --");
                    }
                }

            }
        }
    }
}
