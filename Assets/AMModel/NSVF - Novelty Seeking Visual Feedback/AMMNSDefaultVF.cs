using System;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    /*
     * Novelty Seeking Visual Feedback
     * This class provides default visual feedback on objects according to the Novelty Seeking parameter
     */
    public class AMMNSDefaultVF : AMMEnemy {
        protected override void UpdateVisual(AMMEnemyCategory ec) {
            UpdateNoveltySeeking(ec);

            //StartCoroutine(UpdateVisualTransistionCR(R, 0.1f));
        }

        private void UpdateNoveltySeeking(AMMEnemyCategory ec) {
            foreach (var renderer in RendererLstList) {
                //version 0.1
                foreach (var material in renderer.materials) {
                    try {
                        var color = material.GetColor("_AM_NSOutline");
                        color.a = Mathf.Clamp01(ec.R);
                        material.SetColor("_AM_NSOutline", color);
                    } catch (Exception e) {
                        //TODO :: make this show only once.
                        Debug.Log("-- " + transform.name + "OutlineColor not found. --");
                    }
                }

            }
        }

        //IEnumerator UpdateVisualTransistionCR(float R, float step) {
        //    while (true) {
        //        var color = renderer.material.GetColor("_OutlineColor");
        //        color.a = R * step;
        //        renderer.material.SetColor("_OutlineColor", color);
        //        if (color.a - Mathf.Abs(R) <= step) yield return null;
        //        yield return new WaitForSeconds(0.20f);
        //    }
        //}
    }
}
